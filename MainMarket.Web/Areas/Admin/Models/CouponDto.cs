namespace MainMarket.Web.Areas.Admin.Models;

public class CouponDto
{
    public string? CouponId { get; set; }
    public string CouponCode { get; set; }
    public decimal DiscountAmount { get; set; }
    public int MinAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}