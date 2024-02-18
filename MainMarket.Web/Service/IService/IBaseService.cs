using MainMarket.Web.Models;

namespace MainMarket.Web.Service.IService;

public interface IBaseService
{
    Task<ApiResponse<TResponse>> SendAsync<TRequest, TResponse>(RequestDto<TRequest> couponDto) where TRequest : class where TResponse : class;
}
