using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Product.GetProductsByAuthorName.Common;
using BookShop.API.Controllers.Product.GetProductsByAuthorName.HttpResponseMapper;
using BookShop.Application.Shared.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookShop.API.Controllers.Product.GetProductsByAuthorName.Middleware.Caching;

/// <summary>
///     Filter pipeline for GetProductsByAuthorName caching.
/// </summary>
public class GetProductsByAuthorNameCachingFilter : IAsyncActionFilter
{
    private readonly ICacheHandler _cacheHandler;

    public GetProductsByAuthorNameCachingFilter(ICacheHandler cacheHandler)
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

            var cacheKey = GetProductsByAuthorNameStateBag.GenerateCacheKey(
                parameters: queryParameters,
                request: request
            );
            var cacheModel = await _cacheHandler.GetAsync<GetProductsByAuthorNameHttpResponse>(
                key: cacheKey,
                cancellationToken: CancellationToken.None
            );

            if (
                !Equals(
                    objA: cacheModel,
                    objB: AppCacheModel<GetProductsByAuthorNameHttpResponse>.NotFound
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
                var httpResponse = (GetProductsByAuthorNameHttpResponse)result.Value;

                await _cacheHandler.SetAsync(
                    key: cacheKey,
                    value: httpResponse,
                    distributedCacheEntryOptions: new()
                    {
                        AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(
                            seconds: GetProductsByAuthorNameStateBag.CacheDurationInSeconds
                        )
                    },
                    cancellationToken: CancellationToken.None
                );
            }
        }
    }
}
