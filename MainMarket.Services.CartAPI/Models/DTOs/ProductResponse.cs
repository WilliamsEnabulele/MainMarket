using System.Text.Json.Serialization;

namespace MainMarket.Services.CartAPI.Models.DTO
{
    public class ProductResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName ("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("categoryName")]
        public string CategoryName { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }
        [JsonPropertyName("productImages")]
        public ICollection<string>? ProductImages { get; set; }
    }
}