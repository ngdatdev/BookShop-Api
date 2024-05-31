using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.API.HttpResponseMapper.ErrorApiResponse;
using BookShop.DataAccess.Entities;
using BookShop.DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace BookShop.API.Policy.Authorization;

/// <summary>
///     Handles the custom authorization requirement <see cref="ValidationUserRequirement"/>.
/// </summary>
public class ValidationUserHandler : AuthorizationHandler<ValidationUserRequirement>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ValidationUserHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ValidationUserRequirement requirement
    )
    {
        var httpContext = context.Resource as HttpContext;
        var ct = CancellationToken.None;

        var jtiClaim = httpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Jti);
        await using var scope = _serviceScopeFactory.CreateAsyncScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var isRefreshTokenFound =
            await unitOfWork.RefreshTokenRepository.IsRefreshTokenFoundByAccessTokenIdAsync(
                accessTokenId: Guid.Parse(input: jtiClaim),
                ct
            );

        // Refresh token is not found by access token id.
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
            await unitOfWork.UserDetailRepository.IsUserTemporarilyRemovedAsync(
                id: foundUser.Id,
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
            new ErrorHttpResponse
            {
                HttpCode = statusCode,
                ErrorMessages =
                    statusCode == StatusCodes.Status401Unauthorized
                        ? ["Unauthorized", "Invalid or missing JWT token"]
                        : ["Forbidden", "You don't have permission to access this resource"]
            }
        );

        await context.Response.CompleteAsync();
    }
}
