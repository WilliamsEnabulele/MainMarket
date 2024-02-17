using FluentValidation;
using MainMarket.Services.CouponAPI.Data;
using MainMarket.Services.CouponAPI.Interfaces;
using MainMarket.Services.CouponAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MainMarket.Services.CouponAPI;

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
        services.AddDbContext<CouponContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ICouponRepository, CouponRepository>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        return services;
    }
}