using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.OrderDetail.SwitchOrderStatusToNext.HttpResponseMapper;
using BookShop.Application.Features.Users.UpdateUserById;
using BookShop.Application.Shared.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.API.Controllers.OrderDetail.SwitchOrderStatusToNext.Middleware.Caching;

/// <summary>
///     Filter pipeline for SwitchOrderStatusToNext caching.
/// </summary>
public class SwitchOrderStatusToNextCachingFilter : IAsyncActionFilter
{
    private readonly ICacheHandler _cacheHandler;

    public SwitchOrderStatusToNextCachingFilter(ICacheHandler cacheHandler)
    {
        _cacheHandler = cacheHandler;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        if (!context.HttpContext.Response.HasStarted)
        {
            var request = context.HttpContext.Request;

            var userId = context.HttpContext.User.FindFirstValue(
                claimType: JwtRegisteredClaimNames.Sub
            );

            var cacheKey1 = $"GetAllOrderDetailsByUserId_param_{userId}";

            var cacheKey2 = $"GetOrderDetailById_{request.RouteValues["order-detail-id"]}_{userId}";

            var cacheKey3 =
                $"GetOrderDetailsByOrderStatusId_param_{request.RouteValues["order-status-id"]}_{userId}";

            var executedContext = await next();

            if (executedContext.Result is ObjectResult result)
            {
                var httpResponse = (SwitchOrderStatusToNextHttpResponse)result.Value;

                if (httpResponse.AppCode.Equals(UpdateUserByIdResponseStatusCode.OPERATION_SUCCESS))
                {
                    await _cacheHandler.RemoveAsync(
                        key: cacheKey1,
                        cancellationToken: CancellationToken.None
                    );

                    await _cacheHandler.RemoveAsync(
                        key: cacheKey2,
                        cancellationToken: CancellationToken.None
                    );
                    await _cacheHandler.RemoveAsync(
                        key: cacheKey3,
                        cancellationToken: CancellationToken.None
                    );
                }
            }
        }
    }
}
