namespace MainMarket.Services.CartAPI.Exceptions;

public class ServerErrorException : Exception
{
    public ServerErrorException(string message) : base(message)
    {
    }
}