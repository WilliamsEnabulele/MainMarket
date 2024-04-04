namespace MainMarket.Services.MediaAPI.Configurations;

public class MediaOptions
{
    public int MaxImageFileSize { get; set; }
    public int MaxVideoFileSize { get; set; }
    public int MaxDocumentFileSize { get; set; }
    public string[] AllowedImageFormats { get; set; }
    public string[] AllowedVideoFormats { get; set; }
    public string[] AllowedDocumentFormats { get; set; }
}
