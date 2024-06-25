using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Product.GetAllProducts.Common;
using BookShop.API.Controllers.Product.GetAllProducts.HttpResponseMapper;
using BookShop.Application.Shared.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookShop.API.Controllers.Product.GetAllProducts.Middleware.Caching;

/// <summary>
///     Filter pipeline for GetAllProducts caching.
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
            var cacheKey = $"{nameof(GetAllProductsHttpResponse)}";
            var cacheModel = await _cacheHandler.GetAsync<GetAllProductsHttpResponse>(
                key: cacheKey,
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
                var httpResponse = (GetAllProductsHttpResponse)result.Value;

                await _cacheHandler.SetAsync(
                    key: cacheKey,
                    value: httpResponse,
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
