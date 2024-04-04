using FluentValidation.AspNetCore;
using MainMarket.AuthAPI.Data;
using MainMarket.AuthAPI.Models.Entities;
using MainMarket.AuthAPI.Models.Options;
using MainMarket.AuthAPI.Service;
using MainMarket.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace MainMarket.AuthAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add Fluent Validation
        services.AddFluentValidation(config =>
        config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

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

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
          .AddJwtBearer(options =>
          {
              options.SaveToken = true;
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateAudience = true,
                  ValidateIssuer = true,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  ValidAudience = configuration["EasyBusinessOptions:JwtOptions:Audience"],
                  ValidIssuer = configuration["EasyBusinessOptions:JwtOptions:Issuer"],
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["ApiSettings:JwtOptions:Secret"])),
                  ClockSkew = TimeSpan.Zero
              };
          });

        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "MainMarket Auth API", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter Bearer authentication string as: `Bearer JWT-GENERATED_TOKEN`",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
 {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    Array.Empty<string>()
                } });
        });

        return services;
    }
}