namespace MainMarket.Services.ProductAPI.Models.DTO
{
    public class ProductResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<string> ProductImages { get; set; }
    }
}