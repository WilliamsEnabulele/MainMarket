using MainMarket.Web.Models;
using MainMarket.Web.Service.IService;
using static MainMarket.Web.Util.StaticDetails;

namespace MainMarket.Web.Service;

public class AuthService : IAuthService
{
    private readonly IBaseService _baseService;

    public AuthService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ApiResponse<LoginResponse>> Login(LoginRequest loginRequest)
    {
        return await _baseService.SendAsync<LoginRequest, LoginResponse>(new ApiRequest<LoginRequest>
        {
            ApiType = ApiType.POST,
            Url = AuthAPIBase + "api/auth/login",
            Data = loginRequest
        }, false);
    }

    public async Task<ApiResponse<string>> Register(RegistrationRequest registrationRequestDTO)
    {
        return await _baseService.SendAsync<RegistrationRequest, string>(new ApiRequest<RegistrationRequest>
        {
            ApiType = ApiType.POST,
            Url = AuthAPIBase + "api/auth/register",
            Data = registrationRequestDTO
        }, false);
    }
}