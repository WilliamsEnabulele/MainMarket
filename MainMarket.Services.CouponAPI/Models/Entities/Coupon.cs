using System.ComponentModel.DataAnnotations.Schema;

namespace MainMarket.Services.ProductAPI.Models.Entities;

public class Coupon
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string CouponId { get; set; }

    public string CouponCode { get; set; }
    public decimal DiscountAmount { get; set; }
    public int MinAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}