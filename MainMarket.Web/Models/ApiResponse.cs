namespace MainMarket.Web.Models;

public class ApiResponse<T>
{
    public ApiResponse()
    { }

    public ApiResponse(bool succeeded, T data, IEnumerable<KeyValuePair<string, List<string>>>? errors)
    {
        Succeeded = succeeded;
        Data = data;
        Errors = errors;
    }

    public bool Succeeded { get; set; }

    public T? Data { get; set; }

    public IEnumerable<KeyValuePair<string, List<string>>> Errors { get; set; }

    public static ApiResponse<T> Success(T data)
    {
        return new ApiResponse<T>(true, data, Enumerable.Empty<KeyValuePair<string, List<string>>>());
    }

    public static ApiResponse<T> Failure(IEnumerable<KeyValuePair<string, List<string>>>? errors)
    {

        return new ApiResponse<T>(false, default!, errors?.ToList());
    }
}