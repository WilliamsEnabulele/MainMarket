using MainMarket.Services.CartAPI.Models.DTOs;

namespace MainMarket.Services.CartAPI.Services;

public interface ICartService
{
   public Task<CartResponse> GetCartAsync(string userId);

    public Task<CartResponse> UpSertCart(CartRequest cartRequest);

    public Task<CartResponse> RemoveCartItem(string userId);

    public Task<CartResponse> ApplyCoupon(string couponCode);
}
