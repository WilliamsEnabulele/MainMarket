using MainMarket.Web.Models;

namespace MainMarket.Web.Service.IService;

public interface IBaseService
{
    Task<ApiResponse<TResponse>> SendAsync<TRequest, TResponse>(ApiRequest<TRequest> request, bool bearer = true) where TRequest : class where TResponse : class;
}