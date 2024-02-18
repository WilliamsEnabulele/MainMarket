using MainMarket.AuthAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MainMarket.AuthAPI.Data;

public class AuthContext : IdentityDbContext<AppUser>
{
    public AuthContext(DbContextOptions<AuthContext> options): base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
