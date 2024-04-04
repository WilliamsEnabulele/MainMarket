using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MainMarket.Services.MediaAPI.Configurations;
using MainMarket.Services.MediaAPI.Exceptions;
using Microsoft.Extensions.Options;

namespace MainMarket.Services.MediaAPI.Services;

public class CloudinaryMediaService : IMediaService
{
    private readonly Cloudinary _cloudinary;
    private readonly int _maxImageFileSize;
    private readonly string[] _allowedImageFormats;

    public CloudinaryMediaService()
    {
    }

    public CloudinaryMediaService(
        IOptions<CloudinaryOptions> options,
        IOptions<MediaOptions> mediaOptions)
    {
        var cloudinaryConfig = options.Value;
        var mediaConfig = mediaOptions.Value;

        Account account = new(cloudinaryConfig.CloudName, cloudinaryConfig.ApiKey, cloudinaryConfig.ApiSecret);
        _cloudinary = new Cloudinary(account);

        _maxImageFileSize = mediaConfig.MaxImageFileSize;
        _allowedImageFormats = mediaConfig.AllowedImageFormats;
    }

    public async Task<IEnumerable<string>> AddMediaAsync(IEnumerable<IFormFile> files)
    {
        List<string> urls = new();

        if (files == null)
        {
            return urls;
        }

        await Task.WhenAll(files.Select(file => AddSingleMedia(file, urls)));

        return urls;
    }

    public async Task<string> AddSingleMedia(IFormFile file)
    {
        var urls = new List<string>();
        await AddSingleMedia(file, urls);
        return urls.First();
    }

    public async Task<bool> RemoveMediaAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);
        var deleteResult = _cloudinary.Destroy(deleteParams);

        if (deleteResult.Result != "ok")
        {
            throw new NotFoundException(deleteResult.Result);
        }

        return await Task.FromResult(true);
    }

    private Task AddSingleMedia(IFormFile file, List<string> urls)
    {
        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        var hasValidFormat = _allowedImageFormats.Contains(fileExtension);

        if (file.Length > _maxImageFileSize)
        {
            throw new BadRequestException($"File '{file.FileName}' exceeds the maximum allowed size of {_maxImageFileSize} bytes.");
        }

        if (!hasValidFormat)
        {
            throw new BadRequestException($"File '{file.FileName}' has an unsupported format.");
        }
        try
        {
            var uploadResult = _cloudinary.Upload(new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                PublicId = Guid.NewGuid().ToString(),
            });

            urls.Add(uploadResult.Url.ToString());

            return Task.FromResult(uploadResult);
        }
        catch (Exception ex)
        {
            throw new UploadFailedException($"Failed to upload file '{file.FileName}', {ex}");
        }
    }
}