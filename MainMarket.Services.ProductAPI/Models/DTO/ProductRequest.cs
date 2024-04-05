namespace MainMarket.Services.ProductAPI.Models.DTO;

public class ProductRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string CategoryId { get; set; }
    public string BrandId { get; set; }
    public decimal Price { get; set; }

    public List<ImageRequest> Images { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}