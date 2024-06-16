using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Authentication.Jwt;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using BookShop.Data.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.Auth.Login;

/// <summary>
///     Login Handler
/// </summary>
public class LoginHandler : IFeatureHandler<LoginRequest, LoginResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IRefreshTokenHandler _refreshTokenHandler;
    private readonly IAccessTokenHandler _accessTokenHandler;

    public LoginHandler(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IRefreshTokenHandler refreshTokenHandler,
        IAccessTokenHandler accessTokenHandler
    )
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _signInManager = signInManager;
        _refreshTokenHandler = refreshTokenHandler;
        _accessTokenHandler = accessTokenHandler;
    }

    /// <summary>
    ///     Entry of new request handler.
    /// </summary>
    /// <param name="request">
    ///     Request model.
    /// </param>
    /// <param name="ct">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the response.
    public async Task<LoginResponse> HandlerAsync(
        LoginRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find user by username.
        var foundUser = await _userManager.FindByNameAsync(request.Username);

        // Responds if user does not exist.
        if (Equals(objA: foundUser, objB: default))
        {
            return new() { StatusCode = LoginResponseStatusCode.USER_IS_NOT_FOUND };
        }

        // Check password request .
        var isCorrectPassword = await _userManager.CheckPasswordAsync(
            user: foundUser,
            password: request.Password
        );

        // Responds if password is not correct.
        if (!isCorrectPassword)
        {
            // Check result of locked out.
            var userLockedOutResult = await _signInManager.CheckPasswordSignInAsync(
                user: foundUser,
                password: request.Password,
                lockoutOnFailure: true
            );

            // Responds if out of numbers try to login
            if (userLockedOutResult.IsLockedOut)
            {
                return new() { StatusCode = LoginResponseStatusCode.USER_IS_LOCKED_OUT, };
            }

            return new() { StatusCode = LoginResponseStatusCode.USER_PASSWORD_IS_NOT_CORRECT, };
        }

        // Is user not temporarily removed.
        var isUserNotTemporarilyRemoved =
            await _unitOfWork.AuthFeature.LoginRepository.IsUserTemporarilyRemovedQueryAsync(
                userId: foundUser.Id,
                cancellationToken: cancellationToken
            );

        // Responds if current user is temporarily removed.
        if (!isUserNotTemporarilyRemoved)
        {
            return new() { StatusCode = LoginResponseStatusCode.USER_IS_TEMPORARILY_REMOVED };
        }

        // Get user roles.
        var foundUserRoles = await _userManager.GetRolesAsync(user: foundUser);

        // Init list of user claims.
        List<Claim> userClaims =
        [
            new(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
            new(type: JwtRegisteredClaimNames.Sub, value: foundUser.Id.ToString()),
            new(type: "role", value: foundUserRoles[default])
        ];

        // Generate new refresh token.
        RefreshToken newRefreshToken =
            new()
            {
                AccessTokenId = Guid.Parse(
                    input: userClaims
                        .First(userClaim => userClaim.Type.Equals(JwtRegisteredClaimNames.Jti))
                        .Value
                ),
                RefreshTokenValue = _refreshTokenHandler.Generate(15),
                ExpiredDate = request.IsRemember
                    ? DateTime.UtcNow.AddDays(7)
                    : DateTime.UtcNow.AddDays(3),
                CreatedAt = DateTime.UtcNow,
                UserId = foundUser.Id,
            };

        // Add new refresh token to database.
        var dbResult = await _unitOfWork.AuthFeature.LoginRepository.CreateRefreshTokenCommandAsync(
            refreshToken: newRefreshToken,
            cancellationToken: cancellationToken
        );

        // Response if can't add new refresh token
        if (!dbResult)
        {
            return new() { StatusCode = LoginResponseStatusCode.DATABASE_OPERATION_FAIL };
        }

        // Generate new access token.
        var newAccessToken = _accessTokenHandler.GenerateSigningToken(claims: userClaims);

        var userDetail = await _unitOfWork.AuthFeature.LoginRepository.GetUserDetailByUserIdQueryAsync(
            userId: foundUser.Id,
            cancellationToken: cancellationToken
        );

        // Response successfully.
        return new LoginResponse()
        {
            StatusCode = LoginResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.RefreshTokenValue,
                User = new()
                {
                    Email = foundUser.Email,
                    AvatarUrl = userDetail.AvatarUrl,
                    FirstName = userDetail.FirstName,
                    LastName = userDetail.LastName,
                }
            },
        };
    }
}
