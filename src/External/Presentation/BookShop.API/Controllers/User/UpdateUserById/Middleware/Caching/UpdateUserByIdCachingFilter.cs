using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.Controllers.User.GetAllUsers.HttpResponseMapper;
using BookShop.API.Controllers.User.GetProfileUser.HttpResponseMapper;
using BookShop.API.Controllers.User.UpdateUserById.Common;
using BookShop.API.Controllers.User.UpdateUserById.HttpResponseMapper;
using BookShop.Application.Features.Users.UpdateUserById;
using BookShop.Application.Shared.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookShop.API.Controllers.User.UpdateUserById.Middleware.Caching;

/// <summary>
///     Filter pipeline for UpdateUserById caching.
/// </summary>
public class UpdateUserByIdCachingFilter : IAsyncActionFilter
{
    private readonly ICacheHandler _cacheHandler;

    public UpdateUserByIdCachingFilter(ICacheHandler cacheHandler)
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
                var httpResponse = (UpdateUserByIdHttpResponse)result.Value;

                if (httpResponse.AppCode.Equals(UpdateUserByIdResponseStatusCode.OPERATION_SUCCESS))
                {
                    await _cacheHandler.RemoveAsync(
                        key: $"{nameof(GetAllUsersHttpResponse)}",
                        cancellationToken: CancellationToken.None
                    );

                    await _cacheHandler.RemoveAsync(
                        key: $"{nameof(GetProfileUserHttpResponse)}_{userId}",
                        cancellationToken: CancellationToken.None
                    );
                }
            }
        }
    }
}
