using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Payments.GetAllPayments.Common;
using BookShop.API.Controllers.Payments.GetAllPayments.HttpResponseMapper;
using BookShop.Application.Shared.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookShop.API.Controllers.Payments.GetAllPayments.Middleware.Caching;

/// <summary>
///     Filter pipeline for GetAllPayments caching.
/// </summary>
public class GetAllPaymentsCachingFilter : IAsyncActionFilter
{
    private readonly ICacheHandler _cacheHandler;

    public GetAllPaymentsCachingFilter(ICacheHandler cacheHandler)
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

            var cacheKey = GetAllPaymentsStateBag.GenerateCacheKey(parameters: queryParameters);

            var cacheModel = await _cacheHandler.GetAsync<GetAllPaymentsHttpResponse>(
                key: cacheKey,
                cancellationToken: CancellationToken.None
            );

            if (!Equals(objA: cacheModel, objB: AppCacheModel<GetAllPaymentsHttpResponse>.NotFound))
            {
                context.HttpContext.Response.StatusCode = cacheModel.Value.HttpCode;
                context.Result = new JsonResult(cacheModel.Value);
                return;
            }

            var executedContext = await next();

            if (executedContext.Result is ObjectResult result)
            {
                var httpResponse = (GetAllPaymentsHttpResponse)result.Value;

                await _cacheHandler.SetAsync(
                    key: cacheKey,
                    value: httpResponse,
                    distributedCacheEntryOptions: new()
                    {
                        AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(seconds: 60)
                    },
                    cancellationToken: CancellationToken.None
                );
            }
        }
    }
}
