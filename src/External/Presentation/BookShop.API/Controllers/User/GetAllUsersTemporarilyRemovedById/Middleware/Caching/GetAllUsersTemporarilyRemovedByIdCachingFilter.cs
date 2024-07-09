using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.User.GetAllUsersTemporarilyRemovedById.Common;
using BookShop.API.Controllers.User.GetAllUsersTemporarilyRemovedById.HttpResponseMapper;
using BookShop.Application.Shared.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookShop.API.Controllers.User.GetAllUsersTemporarilyRemovedById.Middleware.Caching;

/// <summary>
///     Filter pipeline for GetAllUsersTemporarilyRemovedById caching.
/// </summary>
public class GetAllUsersTemporarilyRemovedByIdCachingFilter : IAsyncActionFilter
{
    private readonly ICacheHandler _cacheHandler;

    public GetAllUsersTemporarilyRemovedByIdCachingFilter(ICacheHandler cacheHandler)
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
            GetAllUsersTemporarilyRemovedByIdStateBag.CacheKey =
                $"{nameof(GetAllUsersTemporarilyRemovedByIdHttpResponse)}";
            var cacheModel =
                await _cacheHandler.GetAsync<GetAllUsersTemporarilyRemovedByIdHttpResponse>(
                    key: GetAllUsersTemporarilyRemovedByIdStateBag.CacheKey,
                    cancellationToken: CancellationToken.None
                );

            if (
                !Equals(
                    objA: cacheModel,
                    objB: AppCacheModel<GetAllUsersTemporarilyRemovedByIdHttpResponse>.NotFound
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
                var httpResponse = (GetAllUsersTemporarilyRemovedByIdHttpResponse)result.Value;
                await _cacheHandler.SetAsync(
                    key: GetAllUsersTemporarilyRemovedByIdStateBag.CacheKey,
                    value: httpResponse,
                    distributedCacheEntryOptions: new()
                    {
                        AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(
                            seconds: GetAllUsersTemporarilyRemovedByIdStateBag.CacheDurationInSeconds
                        )
                    },
                    cancellationToken: CancellationToken.None
                );
            }
        }
    }
}
