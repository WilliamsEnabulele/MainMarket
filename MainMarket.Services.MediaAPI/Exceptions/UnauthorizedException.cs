namespace MainMarket.Services.MediaAPI.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message) :
        base(message)
    {
    }
}