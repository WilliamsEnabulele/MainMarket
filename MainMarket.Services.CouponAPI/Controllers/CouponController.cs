using MainMarket.Services.ProductAPI.Interfaces;
using MainMarket.Services.ProductAPI.Models;
using MainMarket.Services.ProductAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainMarket.Services.ProductAPI.Controllers;

[Route("api/coupons")]
[ApiController]
public class CouponController : ControllerBase
{
    private readonly ICouponRepository _couponRepository;

    public CouponController(ICouponRepository couponRepository)
    {
        _couponRepository = couponRepository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<CouponDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> GetCoupons()
    {
        var coupons = await _couponRepository.GetCoupons();
        return Ok(ApiResponse<List<CouponDto>>.Success(coupons));
    }

    [HttpGet("{couponId}")]
    [ProducesResponseType(typeof(ApiResponse<CouponDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCouponById(string couponId)
    {
        var coupon = await _couponRepository.GetCoupon(couponId);
        return Ok(ApiResponse<CouponDto>.Success(coupon));
    }

    [HttpGet("{code}")]
    [ProducesResponseType(typeof(ApiResponse<CouponDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCouponByCode(string code)
    {
        var coupon = await _couponRepository.GetCouponByCode(code);
        return Ok(ApiResponse<CouponDto>.Success(coupon));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CouponDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> CreateCoupon([FromBody] CouponDto couponDto)
    {
        var coupon = await _couponRepository.CreateCoupon(couponDto);
        return Ok(ApiResponse<CouponDto>.Success(coupon));
    }

    [HttpDelete("{couponId}")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> DeleteCoupon(string couponId)
    {
        await _couponRepository.DeleteCoupon(couponId);

        return Ok(ApiResponse<string>.Success(string.Empty));
    }

    [HttpPut]
    [ProducesResponseType(typeof(ApiResponse<CouponDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> UpdateCoupon([FromBody] CouponDto couponDto)
    {
        var coupon = await _couponRepository.UpdateCoupon(couponDto);
        return Ok(ApiResponse<CouponDto>.Success(coupon));
    }
}