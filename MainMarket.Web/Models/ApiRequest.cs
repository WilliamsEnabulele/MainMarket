using static MainMarket.Web.Util.StaticDetails;

namespace MainMarket.Web.Models;

public class ApiRequest<TRequest> where TRequest : class
{
    public ApiType ApiType { get; set; }
    public string Url { get; set; }
    public TRequest Data { get; set; }
    public string AccessToken { get; set; }
}