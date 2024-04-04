namespace MainMarket.Services.ProductAPI.Exceptions
{
    public class ServerErrorException : Exception
    {
        public ServerErrorException(string message) : base(message)
        {
        }
    }
}