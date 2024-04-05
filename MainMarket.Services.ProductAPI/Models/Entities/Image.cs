namespace MainMarket.Services.ProductAPI.Models.Entities;

public class Image : BaseEntity
{
    public string ImageUrl { get; set; }

    public string ProductId { get; set; }
}