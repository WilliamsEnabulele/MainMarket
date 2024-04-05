using Bogus;
using MainMarket.Services.ProductAPI.Models.Entities;

namespace MainMarket.Services.ProductAPI.Data;

public static class SeedData
{
    public static List<Category> GenerateCategories(int count)
    {
        var faker = new Faker<Category>();
        return faker.RuleFor(c => c.Id, f => f.Random.Guid().ToString())
                              .RuleFor(c => c.Name, f => f.Random.Word())
                              .RuleFor(c => c.Description, f => f.Random.Word())
                              .Generate(count);
    }

    public static List<Brand> GenerateBrands(int count)
    {
        var faker = new Faker<Brand>();

        return faker
            .RuleFor(b => b.Id, f => f.Random.Guid().ToString())
            .RuleFor(b => b.Name, f => f.Random.Word())
            .Generate(count);
    }

    public static List<Image> GenerateImages(List<Product> products, int count)
    {
        var faker = new Faker<Image>();
        return faker
            .RuleFor(i => i.Id, f => f.Random.Guid().ToString())
            .RuleFor(i => i.ImageUrl, f => f.Person.Website)
            .RuleFor(i => i.ProductId, f => f.PickRandom(products.Select(p => p.Id)))
            .Generate(count);
    }

    public static List<Product> GenerateProducts(List<Category> categories, List<Brand> brands, int count)
    {
        var faker = new Faker<Product>();
        return faker
            .RuleFor(p => p.Id, f => f.Random.Guid().ToString())
            .RuleFor(p => p.BrandId, f => f.PickRandom(brands.Select(b => b.Id)))
            .RuleFor(p => p.CategoryId, f => f.PickRandom(categories.Select(c => c.Id)))
            .RuleFor(p => p.Name, f => f.Commerce.Product())
            .RuleFor(p => p.Price, f => f.Finance.Amount())
            .RuleFor(p => p.Description, f => f.Random.Word())
            .Generate(count);
    }
}