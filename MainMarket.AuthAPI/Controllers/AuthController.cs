using MainMarket.AuthAPI.Models.DTO;
using MainMarket.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace MainMarket.AuthAPI.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(
        IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("register")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Register(RegistrationRequest requestDTO)
    {
        var result = await _authService.Register(requestDTO);
        return Ok(ApiResponse<string>.Success(result));
    }

    [HttpPost]
    [Route("login")]
    [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _authService.Login(request);
        return Ok(ApiResponse<LoginResponse>.Success(result));
    }

    [HttpPost]
    [Route("assign-role")]
    [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AssignRole(AssignRoleRequest request)
    {
        var result = await _authService.AssignRole(request.Email, request.Role);
        return Ok(ApiResponse<bool>.Success(result));
    }
}