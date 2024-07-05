using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.OrderDetail.GetOrderDetailById.Common;
using BookShop.API.Controllers.OrderDetail.GetOrderDetailById.HttpResponseMapper;
using BookShop.Application.Shared.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.API.Controllers.OrderDetail.GetOrderDetailById.Middleware.Caching;

/// <summary>
///     Filter pipeline for GetOrderDetailById caching.
/// </summary>
public class GetOrderDetailByIdCachingFilter : IAsyncActionFilter
{
    private readonly ICacheHandler _cacheHandler;

    public GetOrderDetailByIdCachingFilter(ICacheHandler cacheHandler)
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

            var cacheKey = $"GetOrderDetailById_{request.RouteValues["orderDetail-id"]}_{userId}";
            var cacheModel = await _cacheHandler.GetAsync<GetOrderDetailByIdHttpResponse>(
                key: cacheKey,
                cancellationToken: CancellationToken.None
            );

            if (
                !Equals(
                    objA: cacheModel,
                    objB: AppCacheModel<GetOrderDetailByIdHttpResponse>.NotFound
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
                var httpResponse = (GetOrderDetailByIdHttpResponse)result.Value;

                await _cacheHandler.SetAsync(
                    key: cacheKey,
                    value: httpResponse,
                    distributedCacheEntryOptions: new()
                    {
                        AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(
                            seconds: GetOrderDetailByIdStateBag.CacheDurationInSeconds
                        )
                    },
                    cancellationToken: CancellationToken.None
                );
            }
        }
    }
}
