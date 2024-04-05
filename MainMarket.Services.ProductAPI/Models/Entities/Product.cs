namespace MainMarket.Services.ProductAPI.Models.Entities;

public class Product : BaseEntity
{
    public string CategoryId { get; set; }
    public string BrandId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }

    public Category Category { get; set; }
    public Brand Brand { get; set; }
    public List<Image> Images { get; set; }
}