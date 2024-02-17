namespace MainMarket.Services.CouponAPI.Models;

public class ApiResponse<T>
{
    public ApiResponse()
    { }

    public ApiResponse(bool succeeded, T data, IEnumerable<KeyValuePair<string, List<string>>> errors)
    {
        Succeeded = succeeded;
        Data = data;
        Errors = errors;
    }

    public bool Succeeded { get; set; }

    public T? Data { get; set; }

    public IEnumerable<KeyValuePair<string, List<string>>>? Errors { get; set; }

    public static ApiResponse<T> Success(T data)
    {
        return new ApiResponse<T>(true, data, Enumerable.Empty<KeyValuePair<string, List<string>>>());
    }

    public static ApiResponse<T> Failure(IEnumerable<KeyValuePair<string, List<string>>>? validationErrors, IEnumerable<string>? errors)
    {
        var mergedErrors = new List<KeyValuePair<string, List<string>>>();

        if (validationErrors != null && validationErrors.Any())
        {
            mergedErrors.AddRange(validationErrors);
        }

        if (errors != null && errors.Any())
        {
            mergedErrors.Add(new KeyValuePair<string, List<string>>(string.Empty, errors.ToList()));
        }

        return new ApiResponse<T>(false, default!, mergedErrors);
    }
}