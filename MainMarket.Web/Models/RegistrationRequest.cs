﻿namespace MainMarket.Web.Models;

public class RegistrationRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
}