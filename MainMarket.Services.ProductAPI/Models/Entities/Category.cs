namespace MainMarket.Services.ProductAPI.Models.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
}