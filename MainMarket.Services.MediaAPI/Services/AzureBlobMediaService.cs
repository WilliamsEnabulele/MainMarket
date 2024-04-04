namespace MainMarket.Services.MediaAPI.Services;

public class AzureBlobMediaService : IMediaService
{
    public AzureBlobMediaService()
    {
        
    }

    public Task<IEnumerable<string>> AddMediaAsync(IEnumerable<IFormFile> files)
    {
        throw new NotImplementedException();
    }

    public Task<string> AddSingleMedia(IFormFile file)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveMediaAsync(string publicId)
    {
        throw new NotImplementedException();
    }
}
