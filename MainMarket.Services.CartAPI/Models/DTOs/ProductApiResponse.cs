using System.Text.Json.Serialization;

namespace MainMarket.Services.CartAPI.Models.DTOs;

public class ProductApiResponse<T>
{
    [JsonPropertyName("statusCode")]
    public int StatusCode { get; set; }
    [JsonPropertyName("succeeded")]
    public bool Succeeded { get; set; }
    [JsonPropertyName("data")]
    public T? Data { get; set; }
    [JsonPropertyName("errors")]
    public IEnumerable<string>? Errors { get; set; }
}
