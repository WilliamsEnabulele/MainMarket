using MainMarket.Web.Areas.Admin.Models;
using MainMarket.Web.Models;

namespace MainMarket.Web.Areas.Admin.Service.IService;

public interface ICouponService
{
    Task<ApiResponse<CouponDto>> GetCouponAsync(string couponCode);

    Task<ApiResponse<List<CouponDto>>> GetAllCouponsAsync();

    Task<ApiResponse<CouponDto>> GetCouponByIdAsync(string couponId);

    Task<ApiResponse<CouponDto>> CreateCouponAsync(CouponDto couponDto);

    Task<ApiResponse<CouponDto>> UpdateCouponAsync(CouponDto couponDto);

    Task<ApiResponse<CouponDto>> DeleteCouponAsync(string couponId);
}