using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.User.RemoveUserTemporarilyById;

/// <summary>
///    Implement of command IRemoveUserTemporarilyById repository.
/// </summary>
internal partial class RemoveUserTemporarilyByIdRepository
{
    public async Task<bool> RemoveUserTemporarilyByIdCommandAsync(
        Guid userId,
        DateTime removedAt,
        Guid removedBy,
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
                    _userDetails
                        .Where(predicate: entity => entity.UserId == userId)
                        .ExecuteUpdate(setPropertyCalls: builder =>
                            builder
                                .SetProperty(entity => entity.RemovedAt, removedAt)
                                .SetProperty(entity => entity.RemovedBy, removedBy)
                        );

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
