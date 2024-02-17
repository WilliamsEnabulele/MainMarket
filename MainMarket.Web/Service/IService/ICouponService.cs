using MainMarket.Web.Models;

namespace MainMarket.Web.Service.IService;

public interface ICouponService
{
    Task<ApiResponse<CouponDto>> GetCouponAsync(string couponCode);

    Task<ApiResponse<CouponDto>> GetAllCouponsAsync();

    Task<ApiResponse<CouponDto>> GetCouponByIdAsync(string id);

    Task<ApiResponse<CouponDto>> CreateCouponAsync(RequestDto<CouponDto> requestDto);

    Task<ApiResponse<CouponDto>> UpdateCouponAsync(string id);

    Task<ApiResponse<CouponDto>> DeleteCouponAsync(string id);
}