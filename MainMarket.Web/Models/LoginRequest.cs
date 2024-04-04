using System.ComponentModel.DataAnnotations;

namespace MainMarket.Web.Models;

public class LoginRequest
{
    [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "Please Enter a Valid Email")]
    public string Email { get; set; }
    public string Password { get; set; }
}