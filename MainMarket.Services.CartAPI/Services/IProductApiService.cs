using MainMarket.Services.CartAPI.Models.DTO;
using MainMarket.Services.CartAPI.Models.DTOs;
using Refit;

namespace MainMarket.Services.CartAPI.Services;

[Headers("accept: application/json",
    "Authorization: Bearer")]
public interface IProductApiService
{
    [Get("/api/products/list")]
    Task<ProductApiResponse<List<ProductResponse>>> GetProducts();
}