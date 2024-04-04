using Amazon.S3;
using Amazon.S3.Model;
using MainMarket.Services.MediaAPI.Configurations;
using MainMarket.Services.MediaAPI.Exceptions;
using Microsoft.Extensions.Options;
using System.Net;

namespace MainMarket.Services.MediaAPI.Services;

public class AWSS3MediaService : IMediaService
{
    private readonly IAmazonS3 _s3Client;
    private readonly AWSS3Options _options;
    private readonly int _maxImageFileSize;
    private readonly string[] _allowedImageFormats;

    public AWSS3MediaService()
    {
        
    }

    public AWSS3MediaService(
        IAmazonS3 s3Client,
        IOptions<AWSS3Options> options,
        IOptions<MediaOptions> mediaOptions)
    {
        var mediaConfig = mediaOptions.Value;

        _allowedImageFormats = mediaConfig.AllowedImageFormats;
        _maxImageFileSize = mediaConfig.MaxImageFileSize;
        _s3Client = s3Client;
        _options = options.Value;
    }

    public async Task<IEnumerable<string>> AddMediaAsync(IEnumerable<IFormFile> files)
    {
        var urls = new List<string>();
        var bucketExists = await _s3Client.DoesS3BucketExistAsync(_options.BucketName);

        if (!bucketExists) throw new NotFoundException($"Bucket {_options.BucketName} does not exist.");

        await Task.WhenAll(files.Select(file => AddSingleMedia(file, _s3Client, urls)));

        return urls;
    }

    public async Task<string> AddSingleMedia(IFormFile file)
    {
        var urls = new List<string>();
        await AddSingleMedia(file, _s3Client, urls);
        return urls.First();
    }

    public async Task<bool> RemoveMediaAsync(string publicId)
    {
        var bucketExists = await _s3Client.DoesS3BucketExistAsync(_options.BucketName);

        if (!bucketExists) throw new NotFoundException($"Bucket {_options.BucketName} does not exist");

        var result = await _s3Client.DeleteObjectAsync(_options.BucketName, publicId);

        if (result.HttpStatusCode != HttpStatusCode.OK)
        {
            return false;
        }
        return true;
    }

    private async Task AddSingleMedia(IFormFile file, IAmazonS3 client, List<string> urls)
    {
        if (file.Length > _maxImageFileSize)
        {
            throw new BadRequestException($"File '{file.FileName}' exceeds the maximum allowed size of {_maxImageFileSize} bytes.");
        }

        var hasValidFormat = _allowedImageFormats.Contains(Path.GetExtension(file.FileName).ToLower());

        if (hasValidFormat)
        {
            var request = new PutObjectRequest()
            {
                BucketName = _options.BucketName,
                Key = string.IsNullOrEmpty(_options.Prefix) ? file.FileName : $"{_options.Prefix?.TrimEnd('/')}/{file.FileName}",
                InputStream = file.OpenReadStream()
            };
            request.Metadata.Add("Content-Type", file.ContentType);
            await client.PutObjectAsync(request);
            urls.Add(request.Key);
        }
    }
}