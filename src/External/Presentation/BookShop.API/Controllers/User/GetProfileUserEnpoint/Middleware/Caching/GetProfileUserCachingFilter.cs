using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.User.GetProfileUserEndpoint.Common;
using BookShop.API.Controllers.User.GetProfileUserEndpoint.HttpResponseMapper;
using BookShop.Application.Shared.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookShop.API.Controllers.User.GetProfileUserEndpoint.Middleware.Caching;

/// <summary>
///     Filter pipeline for get profile user caching.
/// </summary>
public class GetProfileUserCachingFilter : IAsyncActionFilter
{
    private readonly ICacheHandler _cacheHandler;

    public GetProfileUserCachingFilter(ICacheHandler cacheHandler)
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
            GetProfileUserStateBag.CacheKey = $"{nameof(GetProfileUserHttpResponse)}";
            var cacheModel = await _cacheHandler.GetAsync<GetProfileUserHttpResponse>(
                key: GetProfileUserStateBag.CacheKey,
                cancellationToken: CancellationToken.None
            );

            if (!Equals(objA: cacheModel, objB: AppCacheModel<GetProfileUserHttpResponse>.NotFound))
            {
                context.HttpContext.Response.StatusCode = cacheModel.Value.HttpCode;
                context.Result = new JsonResult(cacheModel.Value);
                return;
            }

            var executedContext = await next();

            if (executedContext.Result is ObjectResult result)
            {
                await _cacheHandler.SetAsync(
                    key: GetProfileUserStateBag.CacheKey,
                    value: result,
                    distributedCacheEntryOptions: new()
                    {
                        AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(
                            seconds: GetProfileUserStateBag.CacheDurationInSeconds
                        )
                    },
                    cancellationToken: CancellationToken.None
                );
            }
        }
    }
}
