using MainMarket.Web.Service.IService;
using MainMarket.Web.Util;

namespace MainMarket.Web.Service;

public class TokenProvider : ITokenProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void ClearToken()
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Delete(StaticDetails.TokenCookie);
    }

    public string? GetToken()
    {
        var token = string.Empty;
        bool? hasToken = _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(StaticDetails.TokenCookie, out token);
        return hasToken is true ? token : string.Empty;
    }

    public void SetToken(string token)
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Append(StaticDetails.TokenCookie, token);
    }
}