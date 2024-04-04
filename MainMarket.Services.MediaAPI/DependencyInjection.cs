using Amazon.S3;
using MainMarket.Services.MediaAPI.Configurations;
using MainMarket.Services.MediaAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace MainMarket.Services.MediaAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CloudinaryOptions>(configuration.GetSection("Cloudinary"));
        services.Configure<AWSS3Options>(configuration.GetSection("AWSOptions"));
        services.Configure<MediaOptions>(configuration.GetSection("MediaOptions"));

       
        services.AddDefaultAWSOptions(configuration.GetAWSOptions());
        services.AddAWSService<IAmazonS3>();

        services.AddSingleton<CloudinaryMediaService>();
        services.AddSingleton<AzureBlobMediaService>();
        services.AddSingleton<AWSS3MediaService>();
        services.AddSingleton<MediaServiceFactory>();

        services.AddCustomeSwaggerGen();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
   

        return services;
    }

    private static void AddCustomeSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "MainMarket Media API", Version = "v1" });
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