using MainMarket.AuthAPI.Models.DTO;
using MainMarket.AuthAPI.Models.Entities;

namespace MainMarket.AuthAPI.Service.IService;

public interface IAuthService
{
    Task<LoginResponse> Login(LoginRequestDTO requestDTO);
    Task<string> Register (RegistrationRequestDTO requestDTO);
    Task<bool> AssignRole(string email, string role);
}
