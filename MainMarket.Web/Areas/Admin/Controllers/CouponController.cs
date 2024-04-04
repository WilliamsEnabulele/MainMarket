using MainMarket.Web.Areas.Admin.Models;
using MainMarket.Web.Areas.Admin.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainMarket.Web.Areas.Admin.Controllers;

public class CouponController : Controller
{
    private readonly ICouponService _couponService;

    public CouponController(ICouponService couponService)
    {
        _couponService = couponService;
    }

    public async Task<IActionResult> Index()
    {
        var couponList = new List<CouponDto>();
        var response = await _couponService.GetAllCouponsAsync();
        if (response != null && response.Succeeded)
        {
            couponList = response.Data;
        }
        else
        {
            TempData["error"] = response.Errors;
        }
        return View(couponList);
    }

    public async Task<IActionResult> Update(string couponId)
    {
        var response = await _couponService.GetCouponByIdAsync(couponId);
        if (response != null && response.Succeeded)
        {
            return View(response.Data);
        }

        return NotFound();
    }

    public async Task<IActionResult> Delete(string couponId)
    {
        var response = await _couponService.GetCouponByIdAsync(couponId);

        if (response != null && response.Succeeded)
        {
            return View(response.Data);
        }

        return NotFound();
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CouponDto couponDto)
    {
        if (ModelState.IsValid)
        {
            var response = await _couponService.CreateCouponAsync(couponDto);

            if (response.Succeeded)
            {
                TempData["success"] = "Coupon successfully created";
                return RedirectToAction(nameof(Index));
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
            return RedirectToAction(nameof(Index));
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
            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["error"] = response?.Errors;
        }

        return View();
    }
}