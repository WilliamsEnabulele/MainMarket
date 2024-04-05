using MainMarket.Services.CartAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace MainMarket.Services.CartAPI.Data;

public class CartContext : DbContext
{
    public static CartContext Create(IMongoDatabase database) => new(new DbContextOptionsBuilder<CartContext>()
        .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
        .Options);

    public CartContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Cart> Carts { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Cart>().ToCollection("cart");
    }
}