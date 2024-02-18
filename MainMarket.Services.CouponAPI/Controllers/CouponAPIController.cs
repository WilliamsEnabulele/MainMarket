using FluentValidation;
using MainMarket.Services.CouponAPI.Interfaces;
using MainMarket.Services.CouponAPI.Models;
using MainMarket.Services.CouponAPI.Models.DTO;
using MainMarket.Services.CouponAPI.Models.Validation;
using Microsoft.AspNetCore.Mvc;

namespace MainMarket.Services.CouponAPI.Controllers;

[Route("api/coupons")]
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
    [Route("all")]
    [ProducesResponseType(typeof(ApiResponse<List<CouponDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCoupons()
    {
        var coupons = await _couponRepository.GetCoupons();
        return Ok(ApiResponse<List<CouponDto>>.Success(coupons));
    }

    [HttpGet]
    [Route("id/{couponId}")]
    [ProducesResponseType(typeof(ApiResponse<CouponDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCouponById(string couponId)
    {
        var coupon = await _couponRepository.GetCoupon(couponId);
        return Ok(ApiResponse<CouponDto>.Success(coupon));
    }

    [HttpGet]
    [Route("code/{code}")]
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
    [Route("create")]
    [ProducesResponseType(typeof(ApiResponse<CouponDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateCoupon([FromBody] CouponDto couponDto)
    {
        BaseValidator<CouponDto>.Validate(_couponDtoValidator, couponDto);
        var coupon = await _couponRepository.CreateCoupon(couponDto);
        return Ok(ApiResponse<CouponDto>.Success(coupon));
    }

    [HttpDelete]
    [Route("delete/{couponId}")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCoupon(string couponId)
    {
        await _couponRepository.DeleteCoupon(couponId);

        return Ok(ApiResponse<string>.Success(string.Empty));
    }

    [HttpPut]
    [Route("update")]
    [ProducesResponseType(typeof(ApiResponse<CouponDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCoupon([FromBody] CouponDto couponDto)
    {
        BaseValidator<CouponDto>.Validate(_couponDtoValidator, couponDto);
        var coupon = await _couponRepository.UpdateCoupon(couponDto);
        return Ok(ApiResponse<CouponDto>.Success(coupon));
    }
}