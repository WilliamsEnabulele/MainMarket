using MainMarket.AuthAPI.Models.Entities;

namespace MainMarket.AuthAPI.Models.DTO;

public class AssignRoleRequest
{
    public string Email { get; set; }
    public string Role { get; set; }
}
