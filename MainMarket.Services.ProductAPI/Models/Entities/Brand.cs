namespace MainMarket.Services.ProductAPI.Models.Entities;

public class Brand : BaseEntity
{
    public string Name { get; set; }

    public string? LogoUrl { get; set; }
    public string? Description { get; set; }
}