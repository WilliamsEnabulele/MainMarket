namespace MainMarket.Services.MediaAPI.Exceptions;

public class UploadFailedException : Exception
{
    public UploadFailedException(string message) : base(message)
    {
    }
}