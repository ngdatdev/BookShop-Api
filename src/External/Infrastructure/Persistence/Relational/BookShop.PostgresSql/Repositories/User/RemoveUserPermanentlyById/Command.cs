using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.User.RemoveUserPermanentlyById;

/// <summary>
///    Implement of command IRemoveUserPermanentlyById repository.
/// </summary>
internal partial class RemoveUserPermanentlyByIdRepository
{
    public async Task<bool> RemoveUserPermanentlyByIdCommandAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        var dbTransactionResult = false;

        await _context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                using var dbTransaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken: cancellationToken
                );

                try
                {
                    await _carts
                        .Where(predicate: entity => entity.UserId == userId)
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                    await _orders
                        .Where(predicate: entity => entity.UserId == userId)
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                    await _refreshTokens
                        .Where(predicate: entity => entity.UserId == userId)
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                    await _reviews
                        .Where(predicate: entity => entity.UserId == userId)
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                    await _userDetails
                        .Where(predicate: entity => entity.UserId == userId)
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                    await _users
                        .Where(predicate: entity => entity.Id == userId)
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                    await dbTransaction.CommitAsync(cancellationToken: cancellationToken);

                    dbTransactionResult = true;
                }
                catch
                {
                    dbTransactionResult = false;
                }
            });

        return dbTransactionResult;
    }
}
