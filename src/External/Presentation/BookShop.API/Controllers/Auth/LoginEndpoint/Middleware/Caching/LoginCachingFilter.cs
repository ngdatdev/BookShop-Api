using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Auth.LoginEndpoint.Common;
using BookShop.API.Controllers.Auth.LoginEndpoint.HttpResponseMapper;
using BookShop.API.Controllers.Auth.LogoutEndpoint.HttpResponseMapper;
using BookShop.Application.Shared.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookShop.API.Controllers.Auth.LoginEndpoint.Middleware.Caching;

/// <summary>
///     Filter pipeline for login caching.
/// </summary>
public class LoginCachingFilter : IAsyncActionFilter
{
    private readonly ICacheHandler _cacheHandler;

    public LoginCachingFilter(ICacheHandler cacheHandler)
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
            LoginStateBag.CacheKey = $"{nameof(LogoutHttpResponse)}";
            var cacheModel = await _cacheHandler.GetAsync<LogoutHttpResponse>(
                key: LoginStateBag.CacheKey,
                cancellationToken: CancellationToken.None
            );

            if (!Equals(objA: cacheModel, objB: AppCacheModel<LogoutHttpResponse>.NotFound))
            {
                context.HttpContext.Response.StatusCode = cacheModel.Value.HttpCode;
                context.Result = new JsonResult(cacheModel.Value);
                return;
            }

            var executedContext = await next();

            if (executedContext.Result is ObjectResult result)
            {
                await _cacheHandler.SetAsync(
                    key: LoginStateBag.CacheKey,
                    value: result,
                    distributedCacheEntryOptions: new()
                    {
                        AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(
                            seconds: LoginStateBag.CacheDurationInSeconds
                        )
                    },
                    cancellationToken: CancellationToken.None
                );
            }
        }
    }
}
