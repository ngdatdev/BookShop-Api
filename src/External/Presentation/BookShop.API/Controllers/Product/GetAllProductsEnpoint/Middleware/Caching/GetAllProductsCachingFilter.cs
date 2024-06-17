using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Product.GetAllProductsEndpoint.Common;
using BookShop.API.Controllers.Product.GetAllProductsEndpoint.HttpResponseMapper;
using BookShop.Application.Shared.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookShop.API.Controllers.User.GetAllProductsEndpoint.Middleware.Caching;

/// <summary>
///     Filter pipeline for get all products caching.
/// </summary>
public class GetAllProductsCachingFilter : IAsyncActionFilter
{
    private readonly ICacheHandler _cacheHandler;

    public GetAllProductsCachingFilter(ICacheHandler cacheHandler)
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
            GetAllProductsStateBag.CacheKey = $"{nameof(GetAllProductsHttpResponse)}";
            var cacheModel = await _cacheHandler.GetAsync<GetAllProductsHttpResponse>(
                key: GetAllProductsStateBag.CacheKey,
                cancellationToken: CancellationToken.None
            );

            if (!Equals(objA: cacheModel, objB: AppCacheModel<GetAllProductsHttpResponse>.NotFound))
            {
                context.HttpContext.Response.StatusCode = cacheModel.Value.HttpCode;
                context.Result = new JsonResult(cacheModel.Value);
                return;
            }

            var executedContext = await next();

            if (executedContext.Result is ObjectResult result)
            {
                await _cacheHandler.SetAsync(
                    key: GetAllProductsStateBag.CacheKey,
                    value: result,
                    distributedCacheEntryOptions: new()
                    {
                        AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(
                            seconds: GetAllProductsStateBag.CacheDurationInSeconds
                        )
                    },
                    cancellationToken: CancellationToken.None
                );
            }
        }
    }
}
