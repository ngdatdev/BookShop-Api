using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.Review.UpdateReviewById.HttpResponseMapper;
using BookShop.Application.Features.Users.UpdateUserById;
using BookShop.Application.Shared.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.API.Controllers.Review.UpdateReviewById.Middleware.Caching;

/// <summary>
///     Filter pipeline for UpdateReviewById caching.
/// </summary>
public class UpdateReviewByIdCachingFilter : IAsyncActionFilter
{
    private readonly ICacheHandler _cacheHandler;

    public UpdateReviewByIdCachingFilter(ICacheHandler cacheHandler)
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

            var userId = context.HttpContext.User.FindFirstValue(
                claimType: JwtRegisteredClaimNames.Sub
            );

            var cacheKey1 = $"GetReviewsByProductId_param_{request.RouteValues["product-id"]}";

            var cacheKey2 = $"GetReviewsByUserId_{userId}";

            var executedContext = await next();

            if (executedContext.Result is ObjectResult result)
            {
                var httpResponse = (UpdateReviewByIdHttpResponse)result.Value;

                if (httpResponse.AppCode.Equals(UpdateUserByIdResponseStatusCode.OPERATION_SUCCESS))
                {
                    await _cacheHandler.RemoveAsync(
                        key: cacheKey1,
                        cancellationToken: CancellationToken.None
                    );

                    await _cacheHandler.RemoveAsync(
                        key: cacheKey2,
                        cancellationToken: CancellationToken.None
                    );
                }
            }
        }
    }
}
