using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Auth.RefreshAccessToken;

/// <summary>
///     Interface for Command RefreshAccessTokenRepository Repository
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
