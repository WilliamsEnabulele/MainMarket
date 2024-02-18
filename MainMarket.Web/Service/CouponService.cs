using MainMarket.Web.Models;
using MainMarket.Web.Service.IService;
using static MainMarket.Web.Util.StaticDetails;

namespace MainMarket.Web.Service;

public class CouponService : ICouponService
{
    private readonly IBaseService _baseService;

    public CouponService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ApiResponse<CouponDto>> CreateCouponAsync(CouponDto couponDto)
    {
        return await _baseService.SendAsync<CouponDto, CouponDto>(new RequestDto<CouponDto>
        {
            ApiType = ApiType.POST,
            Url = CouponAPIBase + "api/coupons/create",
            Data = couponDto
        });
    }

    public async Task<ApiResponse<CouponDto>> DeleteCouponAsync(string couponId)
    {
        return await _baseService.SendAsync<CouponDto, CouponDto>(new RequestDto<CouponDto>
        {
            ApiType = ApiType.DELETE,
            Url = CouponAPIBase + "api/coupons/delete/" + couponId,
        });
    }

    public async Task<ApiResponse<List<CouponDto>>> GetAllCouponsAsync()
    {
        return await _baseService.SendAsync<CouponDto, List<CouponDto>>(new RequestDto<CouponDto>
        {
            ApiType = ApiType.GET,
            Url = CouponAPIBase + "api/coupons/all"
        });
    }

    public async Task<ApiResponse<CouponDto>> GetCouponAsync(string couponCode)
    {
        return await _baseService.SendAsync<CouponDto, CouponDto>(new RequestDto<CouponDto>
        {
            ApiType = ApiType.GET,
            Url = CouponAPIBase + "api/coupons/code/" + couponCode,
        });
    }

    public async Task<ApiResponse<CouponDto>> GetCouponByIdAsync(string couponId)
    {
        return await _baseService.SendAsync<CouponDto, CouponDto>(new RequestDto<CouponDto>
        {
            ApiType = ApiType.GET,
            Url = CouponAPIBase + "api/coupons/id/" + couponId,
        });
    }

    public async Task<ApiResponse<CouponDto>> UpdateCouponAsync(CouponDto couponDto)
    {
        return await _baseService.SendAsync<CouponDto, CouponDto>(new RequestDto<CouponDto>
        {
            ApiType = ApiType.PUT,
            Url = CouponAPIBase + "api/coupons/update",
            Data = couponDto
        });
    }
}