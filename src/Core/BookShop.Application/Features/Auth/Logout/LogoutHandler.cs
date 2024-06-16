using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.Auth.Logout;

/// <summary>
///     Logout Handler
/// </summary>
public class LogoutHandler : IFeatureHandler<LogoutRequest, LogoutResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LogoutHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
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
    public async Task<LogoutResponse> HandlerAsync(
        LogoutRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get jti claim and convert it to access token id.
        var accessTokenId = Guid.Parse(
            input: _httpContextAccessor.HttpContext.User.FindFirstValue(
                claimType: JwtRegisteredClaimNames.Jti
            )
        );

        // Remove refresh token by access token id.
        var dbResult = await _unitOfWork.AuthFeature.LogoutRepository.RemoveRefreshTokenCommandAsync(
            accessTokenId: accessTokenId,
            cancellationToken: cancellationToken
        );

        // Cannot remove refresh token.
        if (!dbResult)
        {
            return new() { StatusCode = LogoutResponseStatusCode.DATABASE_OPERATION_FAIL };
        }

        return new() { StatusCode = LogoutResponseStatusCode.OPERATION_SUCCESS };
    }
}
