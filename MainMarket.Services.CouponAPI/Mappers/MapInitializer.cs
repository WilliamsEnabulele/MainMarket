using AutoMapper;
using MainMarket.Services.CouponAPI.Models.DTO;
using MainMarket.Services.CouponAPI.Models.Entities;

namespace MainMarket.Services.CouponAPI.Mappers;

public class MapInitializer : Profile
{
    public MapInitializer()
    {
        CreateMap<Coupon, CouponDto>().ReverseMap();
    }
}
