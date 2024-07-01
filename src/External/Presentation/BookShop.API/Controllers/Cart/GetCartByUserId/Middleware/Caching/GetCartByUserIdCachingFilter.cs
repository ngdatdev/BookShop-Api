using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Cart.GetCartByUserId.Common;
using BookShop.API.Controllers.Cart.GetCartByUserId.HttpResponseMapper;
using BookShop.Application.Shared.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookShop.API.Controllers.Cart.GetCartByUserId.Middleware.Caching;

/// <summary>
///     Filter pipeline for GetCartByUserId caching.
/// </summary>
public class GetCartByUserIdCachingFilter : IAsyncActionFilter
{
    private readonly ICacheHandler _cacheHandler;

    public GetCartByUserIdCachingFilter(ICacheHandler cacheHandler)
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
            var userId = context.HttpContext.User.FindFirstValue(
                claimType: JwtRegisteredClaimNames.Sub
            );

            var cacheKey = $"{nameof(GetCartByUserIdHttpResponse)}_{userId}";
            var cacheModel = await _cacheHandler.GetAsync<GetCartByUserIdHttpResponse>(
                key: cacheKey,
                cancellationToken: CancellationToken.None
            );

            if (
                !Equals(objA: cacheModel, objB: AppCacheModel<GetCartByUserIdHttpResponse>.NotFound)
            )
            {
                context.HttpContext.Response.StatusCode = cacheModel.Value.HttpCode;
                context.Result = new JsonResult(cacheModel.Value);
                return;
            }

            var executedContext = await next();

            if (executedContext.Result is ObjectResult result)
            {
                var httpResponse = (GetCartByUserIdHttpResponse)result.Value;

                await _cacheHandler.SetAsync(
                    key: cacheKey,
                    value: httpResponse,
                    distributedCacheEntryOptions: new()
                    {
                        AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(
                            seconds: GetCartByUserIdStateBag.CacheDurationInSeconds
                        )
                    },
                    cancellationToken: CancellationToken.None
                );
            }
        }
    }
}
