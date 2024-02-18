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
            couponList= response.Data;
        }
        else
        {
            TempData["error"] = response.Errors;
        }
        return View(couponList);
    }

    public async Task<IActionResult> UpdateCoupon(string couponId)
    {
        var response = await _couponService.GetCouponByIdAsync(couponId);
        if (response != null && response.Succeeded)
        {
            return View(response.Data);
        }

        return NotFound();
    }

    public async Task<IActionResult> DeleteCoupon(string couponId)
    {
        var response = await _couponService.GetCouponByIdAsync(couponId);

        if (response != null && response.Succeeded)
        {
            return View(response.Data);
        }

        return NotFound();
    }

    public IActionResult CreateCoupon()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCoupon(CouponDto couponDto)
    {      
        if (ModelState.IsValid) {
            var response = await _couponService.CreateCouponAsync(couponDto);

            if (response.Succeeded)
            {
                TempData["success"] = "Coupon successfully created";
                return RedirectToAction(nameof(CouponIndex));
            }
            else
            {
                TempData["error"] = response?.Errors;
            }
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CouponUpdate(CouponDto couponDto)
    {
        var response = await _couponService.UpdateCouponAsync(couponDto);
        if (response != null && response.Succeeded)
        {
            TempData["success"] = "Coupon successfully updated";
            return RedirectToAction(nameof(CouponIndex));
        }
        else
        {
            TempData["error"] = response?.Errors;
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CouponDelete(string couponId)
    {
        var response = await _couponService.DeleteCouponAsync(couponId);

        if (response != null && response.Succeeded)
        {
            TempData["success"] = "Coupon successfully deleted";
            return RedirectToAction(nameof(CouponIndex));
        }
        else
        {
            TempData["error"] = response?.Errors;
        }

        return View();
    }
}