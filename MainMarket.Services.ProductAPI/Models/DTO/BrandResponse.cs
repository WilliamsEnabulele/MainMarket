namespace MainMarket.Services.ProductAPI.Models.DTO;

public class BrandResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string LogoUrl { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
