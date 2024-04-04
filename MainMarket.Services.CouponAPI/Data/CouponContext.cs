using MainMarket.Services.ProductAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MainMarket.Services.ProductAPI.Data;

public class CouponContext : DbContext
{
    public CouponContext(DbContextOptions<CouponContext> options) : base(options)
    {
    }

    public DbSet<Coupon> Coupons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Coupon>().HasData(new Coupon
        {
            CouponId = Guid.NewGuid().ToString(),
            CouponCode = "10OFF",
            DiscountAmount = 10,
            MinAmount = 20,
        });

        modelBuilder.Entity<Coupon>().HasData(new Coupon
        {
            CouponId = Guid.NewGuid().ToString(),
            CouponCode = "20OFF",
            DiscountAmount = 20,
            MinAmount = 40,
        });
    }
}