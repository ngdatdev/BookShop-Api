using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Auth.RefreshAccessToken;

/// <summary>
///    Implement of command IRefreshAccessToken repository.
/// </summary>
internal partial class RefreshAccessTokenRepository
{
    public async Task<bool> UpdateRefreshTokenCommandAsync(
        string oldRefreshTokenValue,
        string newRefreshTokenValue,
        Guid newAccessTokenId,
        CancellationToken cancellationToken
    )
    {
        var executedTransactionResult = false;

        await _context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                using var dbTransaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken: cancellationToken
                );

                try
                {
                    await _refreshTokens
                        .Where(predicate: refreshToken =>
                            refreshToken.RefreshTokenValue == oldRefreshTokenValue
                        )
                        .ExecuteUpdateAsync(setPropertyCalls: builder =>
                            builder
                                .SetProperty(
                                    refreshToken => refreshToken.AccessTokenId,
                                    newAccessTokenId
                                )
                                .SetProperty(
                                    refreshToken => refreshToken.RefreshTokenValue,
                                    newRefreshTokenValue
                                )
                        );
                    await dbTransaction.CommitAsync(cancellationToken: cancellationToken);

                    executedTransactionResult = true;
                }
                catch
                {
                    await dbTransaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });

        return executedTransactionResult;
    }
}
