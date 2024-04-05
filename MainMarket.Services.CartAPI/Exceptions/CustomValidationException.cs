namespace MainMarket.Services.CartAPI.Exceptions;

public class CustomValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public CustomValidationException(IDictionary<string, string[]> failures)
    {
        Errors = failures;
    }
}