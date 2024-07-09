using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Auth.RefreshAccessToken;

/// <summary>
///    Implement of query IRefreshAccessToken repository.
/// </summary>
internal partial class RefreshAccessTokenRepository
{
    public Task<RefreshToken> FindByRefreshTokenValueQueryAsync(
        string refreshTokenValue,
        CancellationToken cancellationToken
    )
    {
        return _refreshTokens
            .AsNoTracking()
            .Where(predicate: refreshToken => refreshToken.RefreshTokenValue == refreshTokenValue)
            .Select(selector: refreshToken => new RefreshToken
            {
                ExpiredDate = refreshToken.ExpiredDate,
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        ;
    }
}
