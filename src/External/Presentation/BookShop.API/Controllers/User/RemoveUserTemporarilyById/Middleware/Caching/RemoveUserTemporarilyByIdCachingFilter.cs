using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.User.RemoveUserTemporarilyById.HttpResponseMapper;
using BookShop.Application.Features.Users.UpdateUserById;
using BookShop.Application.Shared.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.API.Controllers.User.RemoveUserTemporarilyById.Middleware.Caching;

/// <summary>
///     Filter pipeline for RemoveUserTemporarilyById caching.
/// </summary>
public class RemoveUserTemporarilyByIdCachingFilter : IAsyncActionFilter
{
    private readonly ICacheHandler _cacheHandler;

    public RemoveUserTemporarilyByIdCachingFilter(ICacheHandler cacheHandler)
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

            var cacheKey1 = $"GetAllUsersHttpResponse";

            var cacheKey2 = $"GetProfileUserHttpResponse_{userId}";

            var cacheKey3 = "GetAllUsersTemporarilyRemovedByIdHttpResponse";

            var executedContext = await next();

            if (executedContext.Result is ObjectResult result)
            {
                var httpResponse = (RemoveUserTemporarilyByIdHttpResponse)result.Value;

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

                    await _cacheHandler.RemoveAsync(
                        key: cacheKey3,
                        cancellationToken: CancellationToken.None
                    );
                }
            }
        }
    }
}
