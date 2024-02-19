using FluentValidation;
using MainMarket.AuthAPI.Models.DTO;
using MainMarket.AuthAPI.Service.IService;
using MainMarket.Services.AuthAPI.Models.Validation;
using Microsoft.AspNetCore.Mvc;

namespace MainMarket.AuthAPI.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IValidator<RegistrationRequestDTO> _registerValidator;
    private readonly IValidator<LoginRequestDTO> _loginValidator;

    public AuthController(
        IAuthService authService,
        IValidator<RegistrationRequestDTO> registerValidator,
        IValidator<LoginRequestDTO> loginValidator)
    {
        _authService = authService;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
    }

    [HttpPost]
    [Route("register")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Register(RegistrationRequestDTO requestDTO)
    {
        BaseValidator<RegistrationRequestDTO>.Validate(_registerValidator, requestDTO);
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
    public async Task<IActionResult> Login(LoginRequestDTO requestDTO)
    {
        BaseValidator<LoginRequestDTO>.Validate(_loginValidator, requestDTO);
        var result = await _authService.Login(requestDTO);
        return Ok(ApiResponse<LoginResponse>.Success(result));
    }
}