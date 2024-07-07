using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Addresses.RemoveAddressPermanentlyRemovedById;

/// <summary>
///    Implement of command IRemoveAddressPermanentlyRemovedById repository.
/// </summary>
internal partial class RemoveAddressPermanentlyRemovedByIdRepository
{
    public async Task<bool> RemoveAddressPermanentlyRemovedByIdCommandAsync(
        Guid addressId,
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
                        .Where(entity => entity.Id == addressId)
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

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
