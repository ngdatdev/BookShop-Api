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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.Auth.RefreshAccessToken;

/// <summary>
///     RefreshAccessToken Handler
/// </summary>
public class RefreshAccessTokenHandler
    : IFeatureHandler<RefreshAccessTokenRequest, RefreshAccessTokenResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRefreshTokenHandler _refreshTokenHandler;
    private readonly IAccessTokenHandler _accessTokenHandler;

    public RefreshAccessTokenHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        IRefreshTokenHandler refreshTokenHandler,
        IAccessTokenHandler accessTokenHandler
    )
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
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
    public async Task<RefreshAccessTokenResponse> HandlerAsync(
        RefreshAccessTokenRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find refresh token by its value.
        var foundRefreshToken =
            await _unitOfWork.AuthFeature.RefreshAccessTokenRepository.FindByRefreshTokenValueQueryAsync(
                refreshTokenValue: request.RefreshToken,
                cancellationToken: cancellationToken
            );

        // Responds if refresh token is not found.
        if (Equals(objA: foundRefreshToken, objB: default))
        {
            return new RefreshAccessTokenResponse()
            {
                StatusCode = RefreshAccessTokenResponseStatusCode.REFRESH_TOKEN_IS_NOT_FOUND
            };
        }

        // Responds if refresh token is expired.
        if (DateTime.UtcNow > foundRefreshToken.ExpiredDate)
        {
            return new()
            {
                StatusCode = RefreshAccessTokenResponseStatusCode.REFRESH_TOKEN_IS_EXPIRED
            };
        }

        // Init new list of user claims.
        List<Claim> userClaims =
        [
            new(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
            new(
                type: JwtRegisteredClaimNames.Sub,
                value: _httpContextAccessor.HttpContext.User.FindFirstValue(
                    claimType: JwtRegisteredClaimNames.Sub
                )
            ),
            new(
                type: "role",
                value: _httpContextAccessor.HttpContext.User.FindFirstValue(claimType: "role")
            )
        ];

        // Generate new access token.
        var newAccessTokenId = _accessTokenHandler.GenerateSigningToken(claims: userClaims);

        // Generate new refresh token value.
        var newRefreshTokenValue = _refreshTokenHandler.Generate(length: 15);

        // Update current refresh token.
        var dbResult =
            await _unitOfWork.AuthFeature.RefreshAccessTokenRepository.UpdateRefreshTokenCommandAsync(
                oldRefreshTokenValue: request.RefreshToken,
                newRefreshTokenValue: newRefreshTokenValue,
                newAccessTokenId: Guid.Parse(input: userClaims[0].Value),
                cancellationToken: cancellationToken
            );

        // Response if can't add new refresh token
        if (!dbResult)
        {
            return new()
            {
                StatusCode = RefreshAccessTokenResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Response successfully.
        return new RefreshAccessTokenResponse()
        {
            StatusCode = RefreshAccessTokenResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                AccessToken = newAccessTokenId,
                RefreshToken = newRefreshTokenValue
            },
        };
    }
}
