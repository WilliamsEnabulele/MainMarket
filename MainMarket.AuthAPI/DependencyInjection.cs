using FluentValidation;
using MainMarket.AuthAPI.Data;
using MainMarket.AuthAPI.Models.Entities;
using MainMarket.AuthAPI.Models.Options;
using MainMarket.AuthAPI.Service;
using MainMarket.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MainMarket.AuthAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add Fluent Validation
        AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly())
            .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

        // Disable Model State Validation
        services.Configure<ApiBehaviorOptions>(opts =>
        {
            opts.SuppressModelStateInvalidFilter = true;
        });

        // Add DbContext
        services.AddDbContext<AuthContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Add Options
        services.Configure<JwtOptions>(configuration.GetSection("ApiSettings:JwtOptions"));

        // Add Identity
        services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<AuthContext>()
            .AddDefaultTokenProviders();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<IJwtGenerator, JwtGenerator>();
        services.AddScoped<IAuthService, AuthService>();
 

        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        return services;
    }
}