using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.User.GetAllUsers.Common;
using BookShop.API.Controllers.User.GetAllUsers.HttpResponseMapper;
using BookShop.Application.Shared.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookShop.API.Controllers.User.GetAllUsers.Middleware.Caching;

/// <summary>
///     Filter pipeline for GetAllUsers caching.
/// </summary>
public class GetAllUsersCachingFilter : IAsyncActionFilter
{
    private readonly ICacheHandler _cacheHandler;

    public GetAllUsersCachingFilter(ICacheHandler cacheHandler)
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
            GetAllUsersStateBag.CacheKey = $"{nameof(GetAllUsersHttpResponse)}";
            var cacheModel = await _cacheHandler.GetAsync<GetAllUsersHttpResponse>(
                key: GetAllUsersStateBag.CacheKey,
                cancellationToken: CancellationToken.None
            );

            if (!Equals(objA: cacheModel, objB: AppCacheModel<GetAllUsersHttpResponse>.NotFound))
            {
                context.HttpContext.Response.StatusCode = cacheModel.Value.HttpCode;
                context.Result = new JsonResult(cacheModel.Value);
                return;
            }

            var executedContext = await next();

            if (executedContext.Result is ObjectResult result)
            {
                var httpResponse = (GetAllUsersHttpResponse)result.Value;
                await _cacheHandler.SetAsync(
                    key: GetAllUsersStateBag.CacheKey,
                    value: httpResponse,
                    distributedCacheEntryOptions: new()
                    {
                        AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(
                            seconds: GetAllUsersStateBag.CacheDurationInSeconds
                        )
                    },
                    cancellationToken: CancellationToken.None
                );
            }
        }
    }
}
