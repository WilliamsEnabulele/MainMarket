using AutoMapper;
using MainMarket.AuthAPI.Models.DTO;
using MainMarket.AuthAPI.Models.Entities;

namespace MainMarket.Services.AuthAPI.Mappers;

public class MapInitializer : Profile
{
    public MapInitializer()
    {
        CreateMap<RegistrationRequest, AppUser>();
        CreateMap<LoginRequest, AppUser>();
        CreateMap<AppUser, UserDTO>();
    }
}