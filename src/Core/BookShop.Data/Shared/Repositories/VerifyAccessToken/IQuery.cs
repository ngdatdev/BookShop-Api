using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Shared.Repositories.VerifyAccessToken;

/// <summary>
///     Interface for Query VerifyAccessToken Repository
/// </summary>
public partial interface IVerifyAccessTokenRepository
{
    Task<bool> IsRefreshTokenFoundByAccessTokenIdQueryAsync(
        Guid accessTokenId,
        CancellationToken cancellationToken
    );

    Task<bool> IsUserTemporarilyRemovedQueryAsync(Guid userId, CancellationToken cancellationToken);
}
