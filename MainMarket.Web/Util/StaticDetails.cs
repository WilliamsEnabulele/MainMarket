﻿namespace MainMarket.Web.Util;

public class StaticDetails
{
    public static string CouponAPIBase { get; set; }
    public static string AuthAPIBase { get; set; }

    public const string RoleAdmin = "ADMIN";
    public const string RoleCustomer = "CUSTOMER";
    public const string TokenCookie = "JwtToken";

    public enum ApiType
    {
        GET,
        POST,
        PUT,
        DELETE
    }
}