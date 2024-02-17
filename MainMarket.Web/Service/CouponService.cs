using MainMarket.Web.Models;
using MainMarket.Web.Service.IService;
using static MainMarket.Web.Util.StaticDetails;

namespace MainMarket.Web.Service;

public class CouponService : ICouponService
{
    private readonly IBaseService<CouponDto, CouponDto> _baseService;

    public CouponService(IBaseService<CouponDto, CouponDto> baseService)
    {
        _baseService = baseService;
    }

    public async Task<ApiResponse<CouponDto>> CreateCouponAsync(RequestDto<CouponDto> requestDto)
    {
        return await _baseService.SendAsync(new RequestDto<CouponDto>
        {
            ApiType = ApiType.POST,
            Url = CouponAPIBase + "api/coupon/create",
            Data = requestDto.Data,
            AccessToken = requestDto.AccessToken,
        });
    }

    public async Task<ApiResponse<CouponDto>> DeleteCouponAsync(string id)
    {
        return await _baseService.SendAsync(new RequestDto<CouponDto>
        {
            ApiType = ApiType.DELETE,
            Url = CouponAPIBase + "api/delete?" + id,
        });
    }

    public async Task<ApiResponse<CouponDto>> GetAllCouponsAsync()
    {
        return await _baseService.SendAsync(new RequestDto<CouponDto>
        {
            ApiType = ApiType.GET,
            Url = CouponAPIBase + "api/coupon"
        });
    }

    public async Task<ApiResponse<CouponDto>> GetCouponAsync(string couponCode)
    {
        return await _baseService.SendAsync(new RequestDto<CouponDto>
        {
            ApiType = ApiType.GET,
            Url = CouponAPIBase + "api/coupon/code?" + couponCode,
        });
    }

    public async Task<ApiResponse<CouponDto>> GetCouponByIdAsync(string id)
    {
        return await _baseService.SendAsync(new RequestDto<CouponDto>
        {
            ApiType = ApiType.GET,
            Url = CouponAPIBase + "api/coupon/id?" + id,
        });
    }

    public async Task<ApiResponse<CouponDto>> UpdateCouponAsync(string id)
    {
        return await _baseService.SendAsync(new RequestDto<CouponDto>
        {
            ApiType = ApiType.PUT,
            Url = CouponAPIBase + "api/coupon/update?" + id,
        });
    }
}