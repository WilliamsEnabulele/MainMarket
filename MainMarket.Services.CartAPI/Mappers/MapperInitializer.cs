using AutoMapper;
using MainMarket.Services.CartAPI.Models.DTOs;
using MainMarket.Services.CartAPI.Models.Entities;

namespace MainMarket.Services.CartAPI.Mappers;

public class MapInitializer : Profile
{
    public MapInitializer()
    {
        CreateMap<CartRequest, Cart>()
            .ForMember(dest => dest.CartDetails, opts => opts.MapFrom(src => src.CartDetails));
        CreateMap<CartDetailRequest, CartDetail>();
        CreateMap<Cart, CartResponse>()
            .ForMember(dest => dest.CartDetails, opts => opts.MapFrom(src => src.CartDetails));
        CreateMap<CartDetail, CartDetailResponse>();
    }

}