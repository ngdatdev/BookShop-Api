using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Order.CreateOrder.HttpResponseMapper;
using BookShop.Application.Shared.Caching;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookShop.API.Controllers.Order.CreateOrder.Middleware.Caching;

/// <summary>
///     Filter pipeline for CreateOrder caching.
/// </summary>
public class CreateOrderCachingFilter : IAsyncActionFilter
{
    private readonly ICacheHandler _cacheHandler;

    public CreateOrderCachingFilter(ICacheHandler cacheHandler)
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
            var userId = context.HttpContext.User.FindFirstValue(
                claimType: JwtRegisteredClaimNames.Sub
            );

            var executedContext = await next();

            if (executedContext.Result is ObjectResult result)
            {
                var httpResponse = (CreateOrderHttpResponse)result.Value;

                if (httpResponse.HttpCode.Equals(StatusCodes.Status200OK))
                {
                    await _cacheHandler.RemoveAsync(
                        key: $"GetCartByUserIdHttpResponse_{userId}",
                        cancellationToken: CancellationToken.None
                    );
                }
            }
        }
    }
}
