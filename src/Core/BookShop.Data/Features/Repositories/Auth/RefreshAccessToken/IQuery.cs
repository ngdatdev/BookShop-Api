using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Auth.RefreshAccessToken;

/// <summary>
///     Interface for Query RefreshTokenRepository Repository
/// </summary>
public partial interface IRefreshAccessTokenRepository
{
    Task<RefreshToken> FindByRefreshTokenValueQueryAsync(
        string refreshTokenValue,
        CancellationToken cancellationToken
    );
}
