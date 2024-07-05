using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Roles.RemoveRolePermanentlyById;

/// <summary>
///    Implement of query IRemoveRolePermanentlyById repository.
/// </summary>
internal partial class RemoveRolePermanentlyByIdRepository
{
    public async Task<bool> DeleteRolePermanentlyByIdCommandAsync(
        Guid roleId,
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
                        .Where(predicate: entity =>
                            entity.RoleId == roleId
                            && entity.RemovedAt != CommonConstant.MIN_DATE_TIME
                            && entity.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
                        )
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                    await _roles
                        .Where(entity => entity.Id == roleId)
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

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
