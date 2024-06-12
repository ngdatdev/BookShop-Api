using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.CommonResponse;
using BookShop.API.Shared.AppCodes;
using BookShop.Data.Features.UnitOfWork;
using BookShop.Data.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.API.Shared.Filter.AuthorizationFilter;

public class AuthorizationFilter : IAsyncAuthorizationFilter
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public AuthorizationFilter(
        IHttpContextAccessor httpContextAccessor,
        IServiceScopeFactory serviceScopeFactory
    )
    {
        _httpContextAccessor = httpContextAccessor;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext.Response.HasStarted)
        {
            return;
        }
        var ct = CancellationToken.None;

        if (!httpContext.User.Identity.IsAuthenticated)
        {
            await SendResponseAsync(httpContext, StatusCodes.Status401Unauthorized);
            context.Result = new UnauthorizedResult();
            return;
        }

        var jtiClaim = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Jti);

        if (jtiClaim == null)
        {
            await SendResponseAsync(httpContext, StatusCodes.Status401Unauthorized);
            context.Result = new UnauthorizedResult();
            return;
        }

        await using var scope = _serviceScopeFactory.CreateAsyncScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var isRefreshTokenFound =
            await unitOfWork.VerifyAccessTokenRepository.IsRefreshTokenFoundByAccessTokenIdQueryAsync(
                accessTokenId: Guid.Parse(jtiClaim),
                ct
            );

        if (!isRefreshTokenFound)
        {
            await SendResponseAsync(httpContext, StatusCodes.Status403Forbidden);
            context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            return;
        }

        var subClaim = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        var foundUser = await userManager.FindByIdAsync(Guid.Parse(subClaim).ToString());

        if (foundUser == null)
        {
            await SendResponseAsync(httpContext, StatusCodes.Status403Forbidden);
            context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            return;
        }

        var isUserTemporarilyRemoved =
            await unitOfWork.VerifyAccessTokenRepository.IsUserTemporarilyRemovedQueryAsync(
                userId: foundUser.Id,
                cancellationToken: ct
            );

        if (isUserTemporarilyRemoved)
        {
            await SendResponseAsync(httpContext, StatusCodes.Status403Forbidden);
            context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            return;
        }
    }

    private static async Task SendResponseAsync(HttpContext context, int statusCode)
    {
        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsJsonAsync(
            new ApiResponse
            {
                AppCode =
                    statusCode == StatusCodes.Status403Forbidden
                        ? CommonAppCode.FORBIDDEN.ToString()
                        : CommonAppCode.UN_AUTHORIZED.ToString(),
                ErrorMessages =
                    statusCode == StatusCodes.Status403Forbidden
                        ? ["You don't have permission to access this resource"]
                        : ["You need to authentication to access this resource"]
            }
        );

        await context.Response.CompleteAsync();
    }
}
