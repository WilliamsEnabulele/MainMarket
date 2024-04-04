namespace MainMarket.Services.MediaAPI.Services;

public class MediaServiceFactory
{
    private readonly CloudinaryMediaService _mediaService;
    private readonly AWSS3MediaService _amazonMediaService;
    private readonly AzureBlobMediaService _azureBlobMediaService;

    public MediaServiceFactory(
        CloudinaryMediaService mediaService, 
        AWSS3MediaService amazonMediaService, 
        AzureBlobMediaService azureBlobMediaService)
    {
        _mediaService = mediaService;
        _amazonMediaService = amazonMediaService;
        _azureBlobMediaService = azureBlobMediaService;
    }

    public IMediaService Create(string serviceName = null)
    {
        return serviceName.ToLower() switch
        {
            "cloudinary" => _mediaService,
            "aws" => _amazonMediaService,
            "azure" => _azureBlobMediaService,
            _ => _mediaService,
        };
    }
}