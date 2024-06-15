using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Logout;

/// <summary>
///    Implement of command logout repository.
/// </summary>
internal partial class LogoutRepository
{
    public async Task<bool> RemoveRefreshTokenCommandAsync(
        Guid accessTokenId,
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
                            refreshToken.AccessTokenId == accessTokenId
                        )
                        .FirstOrDefaultAsync(cancellationToken: cancellationToken);
                }
                catch
                {
                    await dbTransaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });

        return executedTransactionResult;
    }
}
