using MainMarket.Web.Models;
using MainMarket.Web.Service.IService;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static MainMarket.Web.Util.StaticDetails;

namespace MainMarket.Web.Service;

public class BaseService : IBaseService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITokenProvider _tokenProvider;

    public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
    {
        _httpClientFactory = httpClientFactory;
        _tokenProvider = tokenProvider;
    }

    public async Task<ApiResponse<TResponse>> SendAsync<TRequest, TResponse>(ApiRequest<TRequest> requestDto, bool bearer = true)
        where TRequest : class
        where TResponse : class
    {
        try
        {
            HttpClient client = _httpClientFactory.CreateClient("MainMarket API");

            HttpRequestMessage message = new();

            message.Headers.Add("Accept", "application/json");

            if (bearer)
            {
                var token = _tokenProvider.GetToken();
                message.Headers.Add("Authorization", $"Bearer {token}");
            }
             
            message.RequestUri = new Uri(requestDto.Url);

            if (requestDto.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
            }

            HttpResponseMessage? apiResponse = null;

            message.Method = requestDto.ApiType switch
            {
                ApiType.POST => HttpMethod.Post,
                ApiType.PUT => HttpMethod.Put,
                ApiType.DELETE => HttpMethod.Delete,
                _ => HttpMethod.Get,
            };

            apiResponse = await client.SendAsync(message);
            var apiContent = await apiResponse.Content.ReadAsStringAsync();
            var apiResponseDto = JsonConvert.DeserializeObject<ApiResponse<TResponse>>(apiContent);
            var errors = apiResponseDto?.Errors;
            var validationErrors = apiResponseDto?.ValidationErrors;

            return apiResponse.StatusCode switch
            {
                HttpStatusCode.NotFound => ApiResponse<TResponse>.Failure(errors, (int)HttpStatusCode.NotFound),
                HttpStatusCode.BadRequest => ApiResponse<TResponse>.Failure(validationErrors, (int)HttpStatusCode.BadRequest),
                HttpStatusCode.Unauthorized => ApiResponse<TResponse>.Failure(errors, (int)HttpStatusCode.Unauthorized),
                HttpStatusCode.InternalServerError => ApiResponse<TResponse>.Failure(errors, (int)HttpStatusCode.InternalServerError),
                _ => apiResponseDto,
            };
        }
        catch (Exception)
        {
            throw;
        }
    }
}