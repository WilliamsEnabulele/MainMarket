﻿namespace MainMarket.Services.CouponAPI.Exceptions
{
    public class UnprocessableRequestException : Exception
    {
        public UnprocessableRequestException(string message) : base(message)
        {
        }
    }
}