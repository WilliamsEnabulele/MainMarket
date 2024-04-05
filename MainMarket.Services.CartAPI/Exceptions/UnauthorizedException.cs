namespace MainMarket.Services.CartAPI.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message) :
        base(message)
    {
    }
}