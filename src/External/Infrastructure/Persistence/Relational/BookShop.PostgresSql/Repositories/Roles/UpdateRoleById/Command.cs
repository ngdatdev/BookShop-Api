using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Roles.UpdateRoleById;

/// <summary>
///    Implement of query IUpdateRoleById repository.
/// </summary>
internal partial class UpdateRoleByIdRepository
{
    public async Task<bool> UpdateRoleByIdCommandAsync(
        Guid roleId,
        string roleName,
        DateTime updatedAt,
        Guid updatedBy,
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
                                .SetProperty(entity => entity.UpdatedAt, updatedAt)
                                .SetProperty(entity => entity.UpdatedBy, updatedBy)
                        );

                    await _roles
                        .Where(predicate: entity => entity.Id == roleId)
                        .ExecuteUpdateAsync(setPropertyCalls: builder =>
                            builder.SetProperty(entity => entity.Name, roleName)
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
