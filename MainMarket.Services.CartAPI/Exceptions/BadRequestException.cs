namespace MainMarket.Services.CartAPI.Exceptions;

[Serializable]
public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
    }
}