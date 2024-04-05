using System.ComponentModel.DataAnnotations.Schema;

namespace MainMarket.Services.ProductAPI.Models.Entities;

public class BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
