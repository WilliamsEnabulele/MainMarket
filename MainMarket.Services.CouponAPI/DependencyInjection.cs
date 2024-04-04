using FluentValidation.AspNetCore;
using MainMarket.Services.ProductAPI.Data;
using MainMarket.Services.ProductAPI.Interfaces;
using MainMarket.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace MainMarket.Services.ProductAPI;

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
        services.AddDbContext<CouponContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ICouponRepository, CouponRepository>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

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
                ValidAudience = configuration["ApiSettings:JwtOptions:Audience"],
                ValidIssuer = configuration["ApiSettings:JwtOptions:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["ApiSettings:JwtOptions:Secret"])),
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "MainMarket Coupon API", Version = "v1" });
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