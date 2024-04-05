using MainMarket.Services.CartAPI.Models.DTO;

namespace MainMarket.Services.CartAPI.Models.DTOs;

public class CartDetailResponse
{
    public string ProductId { get; set; }
    public int Count { get; set; }
    public ProductResponse? Product { get; set; }
}