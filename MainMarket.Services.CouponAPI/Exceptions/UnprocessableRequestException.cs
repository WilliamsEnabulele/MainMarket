namespace MainMarket.Services.ProductAPI.Exceptions
{
    public class UnprocessableRequestException : Exception
    {
        public UnprocessableRequestException(string message) : base(message)
        {
        }
    }
}