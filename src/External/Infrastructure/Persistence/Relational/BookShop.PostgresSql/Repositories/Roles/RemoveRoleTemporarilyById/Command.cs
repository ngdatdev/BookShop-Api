using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Roles.RemoveRoleTemporarilyById;

/// <summary>
///    Implement of query IRemoveRoleTemporarilyById repository.
/// </summary>
internal partial class RemoveRoleTemporarilyByIdRepository
{
    public async Task<bool> DeleteRoleTemporarilyByIdCommandAsync(
        Guid roleId,
        Guid removedBy,
        DateTime removedAt,
        CancellationToken cancellationToken
    )
    {
        var dbTransactionResult = false;
        await _context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken: cancellationToken
                );

                try
                {
                    await _roleDetails
                        .Where(predicate: entity => entity.RoleId == roleId)
                        .ExecuteUpdateAsync(setPropertyCalls: builder =>
                            builder
                                .SetProperty(entity => entity.RemovedAt, removedAt)
                                .SetProperty(entity => entity.RemovedBy, removedBy)
                        );

                    await transaction.CommitAsync(cancellationToken: cancellationToken);

                    dbTransactionResult = true;
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });
        return dbTransactionResult;
    }
}
