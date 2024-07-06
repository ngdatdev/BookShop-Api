using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.OrderDetails.RemoveOrderDetailPermanentlyById;

/// <summary>
///    Implement of command IRemoveOrderDetailPermanentlyByIdRepository.
/// </summary>
internal partial class RemoveOrderDetailPermanentlyByIdRepository
{
    public async Task<bool> DeleteOrderDetailPermanentlyByIdCommandAsync(
        Guid orderDetailId,
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
                    await _orderDetails
                        .Where(predicate: entity => entity.OrderId == orderDetailId)
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                    await dbTransaction.CommitAsync(cancellationToken: cancellationToken);

                    dbTransactionResult = true;
                }
                catch
                {
                    await dbTransaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });

        return dbTransactionResult;
    }
}
