using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Addresses.RemoveAddressTemporarilyRemovedById;

/// <summary>
///    Implement of command IRemoveAddressTemporarilyRemovedById repository.
/// </summary>
internal partial class RemoveAddressTemporarilyRemovedByIdRepository
{
    public async Task<bool> RemoveAddressTemporarilyRemovedByIdCommandAsync(
        Guid addressId,
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
                using var transaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken: cancellationToken
                );

                try
                {
                    await _addresses
                        .Where(predicate: address => address.Id == addressId)
                        .ExecuteUpdateAsync(setPropertyCalls: builder =>
                            builder
                                .SetProperty(address => address.RemovedAt, removedAt)
                                .SetProperty(address => address.RemovedBy, removedBy)
                        );

                    dbTransactionResult = true;

                    await transaction.CommitAsync(cancellationToken: cancellationToken);
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });
        return dbTransactionResult;
    }
}
