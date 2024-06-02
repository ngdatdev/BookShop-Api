using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Entities;
using BookShop.Data.Repositories.Interface.Base;

namespace BookShop.Data.Repositories.Interface;

/// <summary>
///     Represent methods that encapsulate queries
///     to interact with "RefreshTokens" table.
/// </summary>
/// <remarks>
///     All repository interfaces must implement
///     <seealso cref="IBaseRepository{TEntity}"/> interface.
/// </remarks>
public interface IRefreshTokenRepository : IBaseRepository<RefreshToken>
{
    Task<bool> IsRefreshTokenFoundByAccessTokenIdAsync(
        Guid accessTokenId,
        CancellationToken cancellationToken
    );
    Task<bool> CreateRefreshTokenAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken
    );
}
