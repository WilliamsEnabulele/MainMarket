using MainMarket.AuthAPI.Models.Entities;

namespace MainMarket.AuthAPI.Service.IService;

public interface IJwtGenerator
{
    string GenerateJwt(AppUser user);
}
