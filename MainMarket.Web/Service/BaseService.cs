using MainMarket.Web.Models;
using MainMarket.Web.Service.IService;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static MainMarket.Web.Util.StaticDetails;

namespace MainMarket.Web.Service;

public class BaseService<TRequest, TResponse> : IBaseService<TRequest, TResponse> where TRequest : class where TResponse : class
{
    private readonly IHttpClientFactory _httpClientFactory;
    public BaseService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ApiResponse<TResponse>> SendAsync(RequestDto<TRequest> requestDto)
    {
        try
        {
            HttpClient client = _httpClientFactory.CreateClient("MainMarket API");
            HttpRequestMessage message = new();
            message.Headers.Add("Accept", "application/json");
            //token
            message.RequestUri = new Uri(requestDto.Url);


            if (requestDto.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");

            }

            HttpResponseMessage? apiResponse = null;
            switch(requestDto.ApiType)
            {
                case ApiType.POST: 
                    message.Method = HttpMethod.Post;
                    break;
                case ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;
                case ApiType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;
                default:
                    message.Method = HttpMethod.Get;
                    break;
            }

            apiResponse = await client.SendAsync(message);
            var apiContent = await apiResponse.Content.ReadAsStringAsync();
            var apiResponseDto = JsonConvert.DeserializeObject<ApiResponse<TResponse>>(apiContent);
            var errors = apiResponseDto.Errors;

            switch (apiResponse.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return ApiResponse<TResponse>.Failure(errors);
                case HttpStatusCode.BadRequest:
                    return ApiResponse<TResponse>.Failure(errors);
                case HttpStatusCode.Unauthorized:
                    return ApiResponse<TResponse>.Failure(errors);
                case HttpStatusCode.InternalServerError:
                    return ApiResponse<TResponse>.Failure(errors);
                default:
                    return apiResponseDto;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

}
