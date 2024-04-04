using AutoMapper;
using MainMarket.Services.ProductAPI.Models.DTO;
using MainMarket.Services.ProductAPI.Models.Entities;

namespace MainMarket.Services.ProductAPI.Mappers;

public class MapInitializer : Profile
{
    public MapInitializer()
    {
        CreateMap<Coupon, CouponDto>().ReverseMap();
    }
}