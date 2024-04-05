using MainMarket.Services.ProductAPI.Models.DTO;
using MainMarket.Services.ProductAPI.Models.Entities;
using MainMarket.Services.ProductAPI.Repository.Interfaces;
using MainMarket.Services.ProductAPI.Services.Interfaces;

namespace MainMarket.Services.ProductAPI.Services;

public class BrandService : IBrandService
{
    private readonly IGenericRepository<Brand> _brandRepository;
    private readonly ILogger<BrandService> _logger;

    public BrandService(
        IGenericRepository<Brand> brandRepository,
        ILogger<BrandService> logger)
    {
        _brandRepository = brandRepository;
        _logger = logger;
    }

    public async Task<BrandResponse> Create(BrandRequest request)
    {
        var brand = new Brand
        {
            Name = request.Name,
            LogoUrl = request.LogoUrl,
            Description = request.Description
        };

        var response = await _brandRepository.Create(brand);

        await _brandRepository.SaveChangesAsync();

        return MapBrandToBrandResponse(response);
    }

    public async Task<bool> DeleteBrand(string id)
    {
        return await _brandRepository.Delete(id);
    }

    public async Task<BrandResponse> GetBrand(string id)
    {
        var response = await _brandRepository.GetById(id);
        return MapBrandToBrandResponse(response);
    }

    public async Task<List<BrandResponse>> GetBrands()
    {
        var responses = await _brandRepository.GetAll();
        return responses
            .Select(brand => MapBrandToBrandResponse(brand))
            .ToList();
    }

    public async Task<BrandResponse> UpdateBrand(BrandRequest request, string id)
    {
        var brand = new Brand
        {
            Name = request.Name,
            LogoUrl = request.LogoUrl,
            Description = request.Description
        };
        var response = await _brandRepository.Update(brand, id);
        await _brandRepository.SaveChangesAsync();
        return MapBrandToBrandResponse(response);
    }

    private static BrandResponse MapBrandToBrandResponse(Brand brand)
    {
        return new BrandResponse
        {
            Id = brand.Id,
            Name = brand.Name,
            LogoUrl = brand.LogoUrl,
            Description = brand.Description,
            CreatedAt = brand.CreatedAt,
            UpdatedAt = brand.UpdatedAt
        };
    }
}