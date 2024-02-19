namespace MainMarket.Services.AuthAPI.Exceptions
{
    public class UnprocessableRequestException : Exception
    {
        public UnprocessableRequestException(string message) : base(message)
        {
        }
    }
}