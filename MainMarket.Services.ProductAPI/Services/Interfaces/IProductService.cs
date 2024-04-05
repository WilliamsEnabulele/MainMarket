using MainMarket.Services.ProductAPI.Models.DTO;

namespace MainMarket.Services.ProductAPI.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductResponse>> GetProducts();

    Task<ProductResponse> GetProductById(string id);

    Task<ProductResponse> CreateProduct(ProductRequest request);

    Task<ProductResponse> UpdateProduct(ProductRequest request, string id);

    Task<bool> DeleteProduct(string id);
}