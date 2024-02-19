using Newtonsoft.Json;

namespace MainMarket.AuthAPI.Models.DTO;

public class ApiResponse<T>
{
    public ApiResponse()
    { }

    public ApiResponse(bool succeeded, T data, IEnumerable<string>? errors)
    {
        Succeeded = succeeded;
        Data = data;
        Errors = errors;
    }

    public ApiResponse(bool succeeded, T data, IDictionary<string, string[]> validationErrors)
    {
        Succeeded = succeeded;
        StatusCode = 200;
        Data = data;
        ValidationErrors = validationErrors;
    }

    public int StatusCode { get; set; }
    public bool Succeeded { get; set; }

    public T? Data { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<string>? Errors { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public IDictionary<string, string[]>? ValidationErrors { get; set; }

    public static ApiResponse<T> Success(T data, IEnumerable<string>? errors = null)
    {
        return new ApiResponse<T>(true, data, errors);
    }

    public static ApiResponse<T> Failure(IDictionary<string, string[]> validationErrors, int statusCode)
    {
        return new ApiResponse<T>
        {
            StatusCode = statusCode,
            Succeeded = false,
            Data = default!,
            ValidationErrors = validationErrors
        };
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