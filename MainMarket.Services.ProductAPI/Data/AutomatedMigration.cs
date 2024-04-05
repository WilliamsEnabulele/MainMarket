using Microsoft.EntityFrameworkCore;

namespace MainMarket.Services.ProductAPI.Data;

public static class AutomatedMigration
{
    public static async Task MigrateAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ProductContext>();

        await context.Database.MigrateAsync();

        if (!context.Categories.Any())
        {
            var categories = SeedData.GenerateCategories(5);
            await context.Categories.AddRangeAsync(categories);
        }

        if (!context.Brands.Any())
        {
            var brands = SeedData.GenerateBrands(10);
            await context.Brands.AddRangeAsync(brands);
        }

        if (!context.Products.Any())
        {
            var categories = await context.Categories.ToListAsync();
            var brands = await context.Brands.ToListAsync();
            var products = SeedData.GenerateProducts(categories, brands, 10);
            await context.Products.AddRangeAsync(products);
        }

        if (!context.Images.Any())
        {
            var products = await context.Products.ToListAsync();
            var images = SeedData.GenerateImages(products, 15);
            await context.Images.AddRangeAsync(images);
        }

        await context.SaveChangesAsync();
    }
}