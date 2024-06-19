using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Auth.Login;

/// <summary>
///    Implement of command login repository.
/// </summary>
internal partial class LoginRepository
{
    public async Task<bool> CreateRefreshTokenCommandAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken
    )
    {
        try
        {
            await _refreshTokens
                .Where(reToken => reToken.UserId.Equals(refreshToken.UserId))
                .ExecuteDeleteAsync(cancellationToken: cancellationToken);

            await _refreshTokens.AddAsync(
                entity: refreshToken,
                cancellationToken: cancellationToken
            );
            await _context.SaveChangesAsync();
        }
        catch
        {
            return false;
        }
        return true;
    }
}
