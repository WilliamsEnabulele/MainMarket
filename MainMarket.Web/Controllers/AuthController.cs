using MainMarket.Web.Models;
using MainMarket.Web.Service.IService;
using MainMarket.Web.Util;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MainMarket.Web.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly ITokenProvider _tokenProvider;

    public AuthController(
        IAuthService authService,
        ITokenProvider tokenProvider)
    {
        _authService = authService;
        _tokenProvider = tokenProvider;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest requestDTO)
    {
        var response = await _authService.Login(requestDTO);

        if (response.Data != null && response.Succeeded)
        {
            var userRole = await SignInUser(response.Data);
            _tokenProvider.SetToken(response.Data.Token);

            if (userRole == StaticDetails.RoleAdmin)
            {
                return RedirectToAction("Index", "Admin", "area");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        TempData["errors"] = response.Errors;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegistrationRequest requestDTO)
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        _tokenProvider.ClearToken();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    private async Task<string> SignInUser(LoginResponse request)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(request.Token);
        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        var userRole = jwt.Claims.FirstOrDefault(u => u.Type == "role").Value;

        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwt.Claims
            .FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims
           .FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
            jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));
        identity.AddClaim(new Claim(ClaimTypes.Name,
            jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));
        identity.AddClaim(new Claim(ClaimTypes.Role, userRole));

        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(new ClaimsPrincipal(principal));

        return userRole;
    }
}