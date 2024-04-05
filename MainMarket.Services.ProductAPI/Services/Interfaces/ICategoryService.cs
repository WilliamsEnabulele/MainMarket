using MainMarket.Services.ProductAPI.Models.DTO;

namespace MainMarket.Services.ProductAPI.Services.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryResponse>> GetCategories();

    Task<CategoryResponse> GetCategoryById(string categoryId);

    Task<CategoryResponse> Create(CategoryRequest request);

    Task<CategoryResponse> Update(CategoryRequest request, string id);

    Task<bool> Delete(string categoryId);
}