namespace MainMarket.Services.MediaAPI.Configurations;

public class AWSS3Options
{
    public string BucketName { get; set; }
    public string PresignedUrl { get; set; }
    public string Prefix { get; set; }
}
