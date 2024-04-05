namespace MainMarket.Services.CartAPI.Exceptions;

public class UnprocessableRequestException : Exception
{
    public UnprocessableRequestException(string message) : base(message)
    {
    }
}