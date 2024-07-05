using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Roles.RestoreRoleById;

/// <summary>
///    Implement of query IRestoreRoleById repository.
/// </summary>
internal partial class RestoreRoleByIdRepository
{
    public async Task<bool> RestoreRoleTemporarilyByIdCommandAsync(
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
                        .Where(predicate: entity => entity.RoleId == roleId)
                        .ExecuteUpdateAsync(setPropertyCalls: builder =>
                            builder
                                .SetProperty(
                                    entity => entity.RemovedAt,
                                    CommonConstant.MIN_DATE_TIME
                                )
                                .SetProperty(
                                    entity => entity.RemovedBy,
                                    CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
                                )
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
