namespace MainMarket.Services.MediaAPI.Exceptions;

public class ServerErrorException : Exception
{
    public ServerErrorException(string message) : base(message)
    {
    }
}