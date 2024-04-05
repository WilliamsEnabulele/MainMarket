using FluentValidation.AspNetCore;
using MainMarket.Services.ProductAPI.Data;
using MainMarket.Services.ProductAPI.Repository;
using MainMarket.Services.ProductAPI.Repository.Interfaces;
using MainMarket.Services.ProductAPI.Services;
using MainMarket.Services.ProductAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

        // Add DbContext
        services.AddDbContext<ProductContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


        // Add Services
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IBrandService, BrandService>();
        // Add Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        // Add Repositories
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddJwtAuthentication(configuration);

        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddCustomSwaggerGen();

        return services;
    }


    private static void AddCustomSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "MainMarket Product API", Version = "v1" });
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
    }


    private static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
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
    }
}