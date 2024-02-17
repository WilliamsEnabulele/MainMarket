using FluentValidation;
using MainMarket.Services.CouponAPI.Interfaces;
using MainMarket.Services.CouponAPI.Models;
using MainMarket.Services.CouponAPI.Models.DTO;
using MainMarket.Services.CouponAPI.Models.Validation;
using Microsoft.AspNetCore.Mvc;

namespace MainMarket.Services.CouponAPI.Controllers;

[Route("api/coupon")]
[ApiController]
public class CouponAPIController : ControllerBase
{
    private readonly ICouponRepository _couponRepository;
    private readonly IValidator<CouponDto> _couponDtoValidator;

    public CouponAPIController(ICouponRepository couponRepository, IValidator<CouponDto> couponDtoValidadtor)
    {
        _couponRepository = couponRepository;
        _couponDtoValidator = couponDtoValidadtor;
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(ApiResponse<List<CouponDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCoupons()
    {
        var coupons = await _couponRepository.GetCoupons();
        return Ok(coupons);
    }

    [HttpGet]
    [Route("Id")]
    [ProducesResponseType(typeof(ApiResponse<CouponDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCouponById([FromQuery] string id)
    {
        var coupon = await _couponRepository.GetCoupon(id);
        return Ok(coupon);
    }

    [HttpGet]
    [Route("Code")]
    [ProducesResponseType(typeof(ApiResponse<CouponDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCouponByCode([FromQuery] string code)
    {
        var coupon = await _couponRepository.GetCouponByCode(code);
        return Ok(coupon);
    }

    [HttpPost]
    [Route("Create")]
    [ProducesResponseType(typeof(ApiResponse<CouponDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateCoupon([FromBody] CouponDto couponDto)
    {
        BaseValidator<CouponDto>.Validate(_couponDtoValidator, couponDto);
        var coupon = await _couponRepository.CreateCoupon(couponDto);
        return Ok(coupon);
    }

    [HttpDelete]
    [Route("Delete")]
    [ProducesResponseType(typeof(ApiResponse<CouponDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCoupon(string id)
    {
        await _couponRepository.DeleteCoupon(id);

        return Ok();
    }

    [HttpPut]
    [Route("Update")]
    [ProducesResponseType(typeof(ApiResponse<CouponDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCoupon([FromBody] CouponDto couponDto)
    {
        BaseValidator<CouponDto>.Validate(_couponDtoValidator, couponDto);
        var coupon = await _couponRepository.UpdateCoupon(couponDto);
        return Ok(coupon);
    }
}