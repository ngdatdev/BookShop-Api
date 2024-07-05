using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.OrderDetail.GetAllOrderDetailsByUserId.Common;
using BookShop.API.Controllers.OrderDetail.GetAllOrderDetailsByUserId.HttpResponseMapper;
using BookShop.Application.Shared.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.API.Controllers.OrderDetail.GetAllOrderDetailsByUserId.Middleware.Caching;

/// <summary>
///     Filter pipeline for GetAllOrderDetailsByUserId caching.
/// </summary>
public class GetAllOrderDetailsByUserIdCachingFilter : IAsyncActionFilter
{
    private readonly ICacheHandler _cacheHandler;

    public GetAllOrderDetailsByUserIdCachingFilter(ICacheHandler cacheHandler)
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

            var cacheKey = $"GetAllOrderDetailsByUserId_param_{userId}";
            var cacheModel = await _cacheHandler.GetAsync<GetAllOrderDetailsByUserIdHttpResponse>(
                key: cacheKey,
                cancellationToken: CancellationToken.None
            );

            if (
                !Equals(
                    objA: cacheModel,
                    objB: AppCacheModel<GetAllOrderDetailsByUserIdHttpResponse>.NotFound
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
                var httpResponse = (GetAllOrderDetailsByUserIdHttpResponse)result.Value;

                await _cacheHandler.SetAsync(
                    key: cacheKey,
                    value: httpResponse,
                    distributedCacheEntryOptions: new()
                    {
                        AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(
                            seconds: GetAllOrderDetailsByUserIdStateBag.CacheDurationInSeconds
                        )
                    },
                    cancellationToken: CancellationToken.None
                );
            }
        }
    }
}
