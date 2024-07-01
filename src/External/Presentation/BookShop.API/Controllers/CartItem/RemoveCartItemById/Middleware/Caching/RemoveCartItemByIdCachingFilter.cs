using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.CartItem.RemoveCartItemById.HttpResponseMapper;
using BookShop.Application.Features.CartItems.RemoveCartItemById;
using BookShop.Application.Shared.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookShop.API.Controllers.CartItem.RemoveCartItemById.Middleware.Caching;

/// <summary>
///     Filter pipeline for RemoveCartItemById caching.
/// </summary>
public class RemoveCartItemByIdCachingFilter : IAsyncActionFilter
{
    private readonly ICacheHandler _cacheHandler;

    public RemoveCartItemByIdCachingFilter(ICacheHandler cacheHandler)
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
                var httpResponse = (RemoveCartItemByIdHttpResponse)result.Value;

                if (
                    httpResponse.AppCode.Equals(
                        RemoveCartItemByIdResponseStatusCode.OPERATION_SUCCESS
                    )
                )
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
