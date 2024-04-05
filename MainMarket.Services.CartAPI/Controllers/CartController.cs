using MainMarket.Services.CartAPI.Models;
using MainMarket.Services.CartAPI.Models.DTOs;
using MainMarket.Services.CartAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MainMarket.Services.CartAPI.Controllers;

[Route("api/cart")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CartResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CartUpsert(CartRequest request)
    {
        return Ok(ApiResponse<CartResponse>.Success(await _cartService.UpSertCart(request)));
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(typeof(ApiResponse<CartResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCartItems(string userId)
    {
        return Ok(ApiResponse<CartResponse>.Success(await _cartService.GetCartAsync(userId)));
    }


}