using Microsoft.AspNetCore.Identity;

namespace MainMarket.AuthAPI.Models.Entities;

public class AppUser : IdentityUser
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
}