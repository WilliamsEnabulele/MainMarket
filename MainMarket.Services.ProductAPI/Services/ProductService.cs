using AutoMapper;
using MainMarket.Services.ProductAPI.Models.DTO;
using MainMarket.Services.ProductAPI.Models.Entities;
using MainMarket.Services.ProductAPI.Repository.Interfaces;
using MainMarket.Services.ProductAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MainMarket.Services.ProductAPI.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductService> _logger;

    public ProductService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<ProductService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ProductResponse> CreateProduct(ProductRequest request)
    {
        var product = _mapper.Map<Product>(request);

        _logger.LogInformation("creating product");

        var res = await _unitOfWork.ProductRepository.Create(product);

        if (request.Images != null)
        {
            _logger.LogInformation("creating images related to product");
            request.Images.Select(async i =>
             {
                 i.ProductId = res.Id;
                 var image = _mapper.Map<Image>(i);
                 await _unitOfWork.ImageRepository.Create(image);
             });
        }
        _logger.LogInformation("saving product");

        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("saved product");

        return _mapper.Map<ProductResponse>(product);
    }

    public async Task<bool> DeleteProduct(string id)
    {
        var response = await _unitOfWork.ProductRepository.Delete(id);
        await _unitOfWork.SaveChangesAsync();
        return response;
    }

    public async Task<ProductResponse> GetProductById(string id)
    {
        var response = await _unitOfWork.ProductRepository.GetById(id);
        return _mapper.Map<ProductResponse>(response);
    }

    public async Task<List<ProductResponse>> GetProducts()
    {
        var response = await _unitOfWork.ProductRepository.GetAll(null, q => q.Include(p => p.Images));
        return _mapper.Map<List<ProductResponse>>(response);
    }

    public async Task<ProductResponse> UpdateProduct(ProductRequest request, string id)
    {
        var product = _mapper.Map<Product>(request);
        var response = await _unitOfWork.ProductRepository.Update(product, id);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ProductResponse>(response);
    }
}