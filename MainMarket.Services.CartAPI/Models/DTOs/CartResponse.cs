namespace MainMarket.Services.CartAPI.Models.DTOs;

public class CartResponse
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string CouponCode { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public List<CartDetailResponse> CartDetails { get; set; }
}