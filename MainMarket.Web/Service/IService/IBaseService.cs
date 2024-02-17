using MainMarket.Web.Models;

namespace MainMarket.Web.Service.IService;

public interface IBaseService<TRequest, TResponse> where TRequest : class where TResponse : class
{
    Task<ApiResponse<TResponse>> SendAsync(RequestDto<TRequest> couponDto);
}
