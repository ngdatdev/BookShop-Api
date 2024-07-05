using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.OrderDetail.GetOrderDetailsByOrderStatusId.Common;
using BookShop.API.Controllers.OrderDetail.GetOrderDetailsByOrderStatusId.HttpResponseMapper;
using BookShop.Application.Shared.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.API.Controllers.OrderDetail.GetOrderDetailsByOrderStatusId.Middleware.Caching;

/// <summary>
///     Filter pipeline for GetOrderDetailsByOrderStatusId caching.
/// </summary>
public class GetOrderDetailsByOrderStatusIdCachingFilter : IAsyncActionFilter
{
    private readonly ICacheHandler _cacheHandler;

    public GetOrderDetailsByOrderStatusIdCachingFilter(ICacheHandler cacheHandler)
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

            var cacheKey =
                $"GetOrderDetailsByOrderStatusId_param_{request.RouteValues["order-status-id"]}_{userId}";
            var cacheModel =
                await _cacheHandler.GetAsync<GetOrderDetailsByOrderStatusIdHttpResponse>(
                    key: cacheKey,
                    cancellationToken: CancellationToken.None
                );

            if (
                !Equals(
                    objA: cacheModel,
                    objB: AppCacheModel<GetOrderDetailsByOrderStatusIdHttpResponse>.NotFound
                )
            )
            {
                context.HttpContext.Response.StatusCode = cacheModel.Value.HttpCode;
                context.Result = new JsonResult(cacheModel.Value);
                return;
            }

            var executedContext = await next();

            if (executedContext.Result is ObjectResult result)
            {
                var httpResponse = (GetOrderDetailsByOrderStatusIdHttpResponse)result.Value;

                await _cacheHandler.SetAsync(
                    key: cacheKey,
                    value: httpResponse,
                    distributedCacheEntryOptions: new()
                    {
                        AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(
                            seconds: GetOrderDetailsByOrderStatusIdStateBag.CacheDurationInSeconds
                        )
                    },
                    cancellationToken: CancellationToken.None
                );
            }
        }
    }
}
