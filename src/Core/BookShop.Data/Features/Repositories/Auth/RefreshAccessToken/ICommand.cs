using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Auth.RefreshAccessToken;

/// <summary>
///     Interface for Command RefreshAccessToken Repository
/// </summary>
public partial interface IRefreshAccessTokenRepository
{
    Task<bool> UpdateRefreshTokenCommandAsync(
        string oldRefreshTokenValue,
        string newRefreshTokenValue,
        Guid newAccessTokenId,
        CancellationToken cancellationToken
    );
}
