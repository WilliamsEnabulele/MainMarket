﻿namespace MainMarket.AuthAPI.Models.DTO;

public class LoginResponse
{
    public UserDTO User { get; set; }
    public string Token { get; set; }
}