using MainMarket.Services.ProductAPI.Models.DTO;

namespace MainMarket.Services.ProductAPI.Services.Interfaces;

public interface IBrandService
{
    Task<BrandResponse> GetBrand(string id);

    Task<List<BrandResponse>> GetBrands();

    Task<BrandResponse> Create(BrandRequest request);

    Task<BrandResponse> UpdateBrand(BrandRequest request, string id);

    Task<bool> DeleteBrand(string id);
}