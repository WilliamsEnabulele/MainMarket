using FluentValidation.AspNetCore;
using MainMarket.Services.CartAPI.Options;
using MainMarket.Services.CartAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Refit;
using System.Reflection;

namespace MainMarket.Services.CartAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICartService, CartService>();

        var authToken = "";
        var refitSettings = new RefitSettings()
        {
            AuthorizationHeaderValueGetter = (rq, ct) => Task.FromResult(authToken)
        };

        services
        .AddRefitClient<IProductApiService>(refitSettings)
        .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["ServiceUrls:ProductAPI"]));

        services.AddCustomSwaggerGen();

        services.AddFluentValidation(config =>
        config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.Configure<MongoOptions>(configuration.GetSection("MongoOptions"));
        return services;
    }

    private static void AddCustomSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "MainMarket Cart API", Version = "v1" });
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
}