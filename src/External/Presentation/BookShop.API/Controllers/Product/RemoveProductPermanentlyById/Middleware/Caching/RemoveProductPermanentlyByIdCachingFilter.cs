using System.Threading.Tasks;
using BookShop.API.Controllers.Product.RemoveProductPermanentlyById.HttpResponseMapper;
using BookShop.Application.Shared.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookShop.API.Controllers.Product.RemoveProductPermanentlyById.Middleware.Caching;

/// <summary>
///     Filter pipeline for RemoveProductPermanentlyById caching.
/// </summary>
public class RemoveProductPermanentlyByIdCachingFilter : IAsyncActionFilter
{
    private readonly ICacheHandler _cacheHandler;

    public RemoveProductPermanentlyByIdCachingFilter(ICacheHandler cacheHandler)
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
            var executedContext = await next();

            if (executedContext.Result is ObjectResult result)
            {
                var httpResponse = (RemoveProductPermanentlyByIdHttpResponse)result.Value;
            }
        }
    }
}
