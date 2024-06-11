using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.AppCodes;
using BookShop.API.CommonResponse;
using BookShop.Data.Features.UnitOfWork;
using BookShop.Data.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.API.Shared.Policy.Authorization;

/// <summary>
///     Handles the custom authorization requirement <see cref="ValidationUserRequirement"/>.
/// </summary>
public class VerifyAccessTokenHandler : AuthorizationHandler<VerifyAccessTokenRequirement>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public VerifyAccessTokenHandler(
        IServiceScopeFactory serviceScopeFactory,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _serviceScopeFactory = serviceScopeFactory;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        VerifyAccessTokenRequirement requirement
    )
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext.Response.HasStarted)
        { 
            return;
        }
        var ct = CancellationToken.None;

        if (httpContext == null || !httpContext.User.Identity.IsAuthenticated)
        {
            await SendResponseAsync(httpContext, StatusCodes.Status401Unauthorized);
            return;
        }

        var jtiClaim = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Jti);

        if (jtiClaim == null)
        {
            await SendResponseAsync(httpContext, StatusCodes.Status401Unauthorized);
            return;
        }

        await using var scope = _serviceScopeFactory.CreateAsyncScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var isRefreshTokenFound =
            await unitOfWork.VerifyAccessTokenRepository.IsRefreshTokenFoundByAccessTokenIdQueryAsync(
                accessTokenId: Guid.Parse(input: jtiClaim),
                ct
            );

        if (!isRefreshTokenFound)
        {
            await SendResponseAsync(
                statusCode: StatusCodes.Status403Forbidden,
                context: httpContext
            );
        }

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        // Get the sub claim from the access token.
        var subClaim = httpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub);

        // Find user by user id.
        var foundUser = await userManager.FindByIdAsync(
            userId: Guid.Parse(input: subClaim).ToString()
        );

        // User is not found
        if (Equals(objA: foundUser, objB: default))
        {
            await SendResponseAsync(
                statusCode: StatusCodes.Status403Forbidden,
                context: httpContext
            );
        }

        // Is user temporarily removed.
        var isUserTemporarilyRemoved =
            await unitOfWork.VerifyAccessTokenRepository.IsUserTemporarilyRemovedQueryAsync(
                userId: foundUser.Id,
                cancellationToken: ct
            );

        // User is temporarily removed.
        if (isUserTemporarilyRemoved)
        {
            await SendResponseAsync(
                statusCode: StatusCodes.Status403Forbidden,
                context: httpContext
            );
        }

        // Get the role claim from the access token.
        // var roleClaim = httpContext.User.FindFirstValue(claimType: "role");

        // var isUserInRole = await userManager.IsInRoleAsync(user: foundUser, role: roleClaim);
        // if (!isUserInRole)
        // {
        //     await SendResponseAsync(statusCode: StatusCodes.Status403Forbidden, context: context);
        // }

        context.Succeed(requirement);
    }

    private static async Task SendResponseAsync(HttpContext context, int statusCode)
    {
        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsJsonAsync(
            statusCode == StatusCodes.Status403Forbidden
                ? new ApiResponse
                {
                    AppCode = CommonAppCode.FORBIDDEN.ToString(),
                    ErrorMessages = ["You don't have permission to access this resource"]
                }
                : new ApiResponse
                {
                    AppCode = CommonAppCode.UN_AUTHORIZED.ToString(),
                    ErrorMessages = ["You need to authentication to access this resource"]
                }
        );

        await context.Response.CompleteAsync();
    }
}
