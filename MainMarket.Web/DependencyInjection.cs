using MainMarket.Web.Areas.Admin.Service;
using MainMarket.Web.Areas.Admin.Service.IService;
using MainMarket.Web.Service;
using MainMarket.Web.Service.IService;
using MainMarket.Web.Util;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MainMarket.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews();
        services.AddHttpContextAccessor();
        services.AddHttpClient();

        services.AddHttpClient<ICouponService, CouponService>();
        services.AddHttpClient<IAuthService, AuthService>();

        StaticDetails.CouponAPIBase = configuration["ServiceUrls:CouponAPI"];
        StaticDetails.AuthAPIBase = configuration["ServiceUrls:AuthAPI"];

        services.AddScoped<IBaseService, BaseService>();
        services.AddScoped<ICouponService, CouponService>();
        services.AddScoped<ITokenProvider, TokenProvider>();
        services.AddScoped<IAuthService, AuthService>();

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromHours(10);
                options.LoginPath = "/Auth/Login";
                options.AccessDeniedPath = "/Auth/AccessDenied";
            });

        return services;
    }
}