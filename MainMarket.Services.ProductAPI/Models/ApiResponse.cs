﻿namespace MainMarket.Services.ProductAPI.Models;

public class ApiResponse<T>
{
    public ApiResponse()
    { }

    public ApiResponse(bool succeeded, T data, IEnumerable<string>? errors, int statusCode)
    {
        StatusCode = statusCode;
        Succeeded = succeeded;
        Data = data;
        Errors = errors;
    }

    public int StatusCode { get; set; }
    public bool Succeeded { get; set; }

    public T? Data { get; set; }
    public IEnumerable<string>? Errors { get; set; }

    public static ApiResponse<T> Success(T data, IEnumerable<string>? errors = null, int statusCode = 200)
    {
        return new ApiResponse<T>(true, data, errors, statusCode);
    }

    public static ApiResponse<T> Failure(IEnumerable<string>? errors, int statusCode)
    {
        return new ApiResponse<T>
        {
            StatusCode = statusCode,
            Succeeded = false,
            Data = default!,
            Errors = errors
        };
    }
}