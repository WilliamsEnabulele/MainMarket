using MainMarket.Web.Models;

namespace MainMarket.Web.Service.IService;

public interface IAuthService
{
    Task<ApiResponse<LoginResponse>> Login(LoginRequest loginRequestDTO);

    Task<ApiResponse<string>> Register(RegistrationRequest registrationRequestDTO);
}