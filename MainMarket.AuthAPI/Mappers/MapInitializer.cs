using AutoMapper;
using MainMarket.AuthAPI.Models.DTO;
using MainMarket.AuthAPI.Models.Entities;

namespace MainMarket.Services.AuthAPI.Mappers;

public class MapInitializer : Profile
{
    public MapInitializer()
    {
        CreateMap<RegistrationRequestDTO, AppUser>();
        CreateMap<LoginRequestDTO, AppUser>();
        CreateMap<AppUser, UserDTO>();
    }
}