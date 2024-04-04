using MainMarket.AuthAPI.Models.Entities;
using MainMarket.AuthAPI.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MainMarket.AuthAPI.Data;

public class AuthContext : IdentityDbContext<AppUser>
{
    public AuthContext(DbContextOptions<AuthContext> options) : base(options)
    {
    }

    public DbSet<AppUser> AppUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Name = StaticDetails.Roles.ADMIN.ToString(),
            NormalizedName = StaticDetails.Roles.ADMIN.ToString()

        });

        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Name = StaticDetails.Roles.CUSTOMER.ToString(),
            NormalizedName  = StaticDetails.Roles.CUSTOMER.ToString()
        });

        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Name = StaticDetails.Roles.BUSINESS.ToString(),
            NormalizedName = StaticDetails.Roles.BUSINESS.ToString()
        });
    }
}
