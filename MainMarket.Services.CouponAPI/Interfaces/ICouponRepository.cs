using MainMarket.Services.CouponAPI.Models.DTO;

namespace MainMarket.Services.CouponAPI.Interfaces;

public interface ICouponRepository
{
    public Task<List<CouponDto>> GetCoupons();

    public Task<CouponDto> GetCoupon(string Id);

    public Task<CouponDto> GetCouponByCode(string code);

    public Task<CouponDto> CreateCoupon(CouponDto couponDto);

    public Task<CouponDto> UpdateCoupon(CouponDto couponDto);

    public Task DeleteCoupon(string Id);
}