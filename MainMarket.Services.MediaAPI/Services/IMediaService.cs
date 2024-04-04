namespace MainMarket.Services.MediaAPI.Services;

public interface IMediaService
{
    Task<string> AddSingleMedia(IFormFile file);
    Task<IEnumerable<string>> AddMediaAsync(IEnumerable<IFormFile> files);
    Task<bool> RemoveMediaAsync(string publicId);
}
