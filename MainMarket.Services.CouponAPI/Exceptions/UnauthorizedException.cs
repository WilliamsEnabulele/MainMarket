namespace MainMarket.Services.ProductAPI.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) :
            base(message)
        {
        }
    }
}