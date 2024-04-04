using MainMarket.AuthAPI.Models.DTO;

namespace MainMarket.AuthAPI.Service.IService;

public interface IAuthService
{
    Task<LoginResponse> Login(LoginRequest requestDTO);

    Task<string> Register(RegistrationRequest requestDTO);

    Task<bool> AssignRole(string email, string role);
}