using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Product.GetProductsByCategoryIdEndpoint.Common;
using BookShop.API.Controllers.Product.GetProductsByCategoryIdEndpoint.HttpResponseMapper;
using BookShop.Application.Shared.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookShop.API.Controllers.Product.GetProductsByCategoryIdEndpoint.Middleware.Caching;

/// <summary>
///     Filter pipeline for GetProductsByCategoryId caching.
/// </summary>
public class GetProductsByCategoryIdCachingFilter : IAsyncActionFilter
{
    private readonly ICacheHandler _cacheHandler;

    public GetProductsByCategoryIdCachingFilter(ICacheHandler cacheHandler)
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
            var queryParameters = request.Query.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.ToString()
            );

            var cacheKey = GetProductsByCategoryIdStateBag.GenerateCacheKey(queryParameters);

            var cacheModel = await _cacheHandler.GetAsync<GetProductsByCategoryIdHttpResponse>(
                key: cacheKey,
                cancellationToken: CancellationToken.None
            );

            if (
                !Equals(
                    objA: cacheModel,
                    objB: AppCacheModel<GetProductsByCategoryIdHttpResponse>.NotFound
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
                var httpResponse = (GetProductsByCategoryIdHttpResponse)result.Value;

                await _cacheHandler.SetAsync(
                    key: cacheKey,
                    value: httpResponse,
                    distributedCacheEntryOptions: new()
                    {
                        AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(
                            seconds: GetProductsByCategoryIdStateBag.CacheDurationInSeconds
                        )
                    },
                    cancellationToken: CancellationToken.None
                );
            }
        }
    }
}
