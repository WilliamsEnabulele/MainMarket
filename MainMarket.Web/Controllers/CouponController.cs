using MainMarket.Web.Models;
using MainMarket.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace MainMarket.Web.Controllers;

public class CouponController : Controller
{
    private readonly ICouponService _couponService;

    public CouponController(ICouponService couponService)
    {
        _couponService = couponService;
    }

    public async Task<IActionResult> CouponIndex()
    {
        var couponList = new List<CouponDto>();
        var response = await _couponService.GetAllCouponsAsync();
        if (response != null && response.Succeeded)
        {
            couponList.Add(response.Data);
        }
        return View(couponList);
    }
}