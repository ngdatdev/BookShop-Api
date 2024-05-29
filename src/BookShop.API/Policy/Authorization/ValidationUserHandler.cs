using System;
using System.Collections.Generic;
using System.Linq;
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
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ValidationUserHandler(
        TokenValidationParameters tokenValidationParameters,
        IServiceScopeFactory serviceScopeFactory
    )
    {
        _tokenValidationParameters = tokenValidationParameters;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext handlerContext,
        ValidationUserRequirement requirement
    )
    {
        JsonWebTokenHandler jsonWebTokenHandler = new();
        var context = handlerContext.Resource as HttpContext;
        var ct = CancellationToken.None;

        var jtiClaim = context.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Jti);
        await using var scope = _serviceScopeFactory.CreateAsyncScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var isRefreshTokenFound =
            await unitOfWork.RefreshTokenRepository.IsRefreshTokenFoundByAccessTokenIdQueryAsync(
                accessTokenId: Guid.Parse(input: jtiClaim)
            );

        // Refresh token is not found by access token id.
        if (!isRefreshTokenFound)
        {
            await SendResponseAsync(statusCode: StatusCodes.Status403Forbidden, context: context);
        }

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        // Get the sub claim from the access token.
        var subClaim = context.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub);

        // Find user by user id.
        var foundUser = await userManager.FindByIdAsync(
            userId: Guid.Parse(input: subClaim).ToString()
        );

        // User is not found
        if (Equals(objA: foundUser, objB: default))
        {
            await SendResponseAsync(statusCode: StatusCodes.Status403Forbidden, context: context);
        }

        // Is user temporarily removed.
        var isUserTemporarilyRemoved =
            await unitOfWork.UserDetailRepository.IsUserTemporarilyRemovedAsync(id: foundUser.Id, cancellationToken: ct);

        // User is temporarily removed.
        if (isUserTemporarilyRemoved)
        {
            await SendResponseAsync(statusCode: StatusCodes.Status403Forbidden, context: context);
        }

        // Get the role claim from the access token.
        var roleClaim = context.User.FindFirstValue(claimType: "role");

        // Is user in role.
        var isUserInRole = await userManager.IsInRoleAsync(user: foundUser, role: roleClaim);
        if (!isUserInRole)
        {
            await SendResponseAsync(statusCode: StatusCodes.Status403Forbidden, context: context);
        }
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
