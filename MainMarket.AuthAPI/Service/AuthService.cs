using AutoMapper;
using MainMarket.AuthAPI.Data;
using MainMarket.AuthAPI.Models.DTO;
using MainMarket.AuthAPI.Models.Entities;
using MainMarket.AuthAPI.Service.IService;
using MainMarket.Services.AuthAPI.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace MainMarket.AuthAPI.Service;

public class AuthService : IAuthService
{
    private readonly AuthContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IMapper _mapper;

    public AuthService(
        AuthContext context,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IJwtGenerator jwtGenerator,
        IMapper mapper)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtGenerator = jwtGenerator;
        _mapper = mapper;
    }

    public async Task<bool> AssignRole(string email, string roleName)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email) ?? throw new NotFoundException("no user with email exist");
        var roleExist = await _roleManager.RoleExistsAsync(roleName);
        if (roleExist)
        {
            await _userManager.AddToRoleAsync(user, roleName);
            return true;
        }
        return false;
    }

    public async Task<LoginResponse> Login(LoginRequest requestDTO)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == requestDTO.Email) ?? throw new NotFoundException("No user with email exist");
        bool isValid = await _userManager.CheckPasswordAsync(user, requestDTO.Password);
        if (!isValid) throw new NotFoundException("Password is incorrect");

        var roles = await _userManager.GetRolesAsync(user);

        return new LoginResponse
        {
            User = _mapper.Map<UserDTO>(user),
            Token = _jwtGenerator.GenerateJwt(user, roles)
        };
    }

    public async Task<string> Register(RegistrationRequest requestDTO)
    {
        var user = _mapper.Map<AppUser>(requestDTO);
        user.UserName = requestDTO.Email;

        var response = await _userManager.CreateAsync(user, requestDTO.Password);
        if (response.Succeeded)
        {
            return string.Empty;
        }
        else
        {
            throw new UnprocessableRequestException(response.Errors.FirstOrDefault().Description);
        }
    }
}