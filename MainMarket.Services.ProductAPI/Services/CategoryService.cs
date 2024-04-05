using MainMarket.Services.ProductAPI.Models.DTO;
using MainMarket.Services.ProductAPI.Models.Entities;
using MainMarket.Services.ProductAPI.Repository.Interfaces;
using MainMarket.Services.ProductAPI.Services.Interfaces;

namespace MainMarket.Services.ProductAPI.Services;

public class CategoryService : ICategoryService
{
    private readonly IGenericRepository<Category> _categoryRepository;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(ILogger<CategoryService> logger,
        IGenericRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
        _logger = logger;
    }

    public async Task<CategoryResponse> Create(CategoryRequest request)
    {
        _logger.LogInformation("creating product category");

        var response = await _categoryRepository.Create(new Category
        {
            Name = request.Name,
            Description = request.Description
        });

        _logger.LogInformation("created product category");

        return MapCategoryResponse(response);
    }

    public async Task<bool> Delete(string categoryId)
    {
        return await _categoryRepository.Delete(categoryId);
    }

    public Task<List<CategoryResponse>> GetCategories()
    {
        _logger.LogInformation("getting categories");

        var categories = _categoryRepository.GetAll().GetAwaiter().GetResult();

        _logger.LogInformation("return category response");

        return Task.FromResult(categories.Select(category => MapCategoryResponse(category)).ToList());
    }

    public async Task<CategoryResponse> GetCategoryById(string categoryId)
    {
        _logger.LogInformation("Get category by id");
        var response = await _categoryRepository.GetById(categoryId);

        return MapCategoryResponse(response);
    }

    public async Task<CategoryResponse> Update(CategoryRequest request, string id)
    {
        var response = await _categoryRepository.Update(new Category
        {
            Name = request.Name,
            Description = request.Description
        }, id);

        return MapCategoryResponse(response);
    }

    private CategoryResponse MapCategoryResponse(Category category)
    {
        return new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };
    }
}