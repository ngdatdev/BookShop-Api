using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.OrderDetails.SwitchOrderStatusToCancel;

/// <summary>
///    Implement of command ISwitchOrderStatusToCancel repository.
/// </summary>
internal partial class SwitchOrderStatusToCancelRepository
{
    public async Task<bool> SwitchOrderStatusToCancelCommandAsync(
        Guid orderDetailId,
        Guid newOrderStatusId,
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
                using var dbTransaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken: cancellationToken
                );

                try
                {
                    _orderDetails
                        .Where(predicate: orderDetail => orderDetail.Id == orderDetailId)
                        .ExecuteUpdate(setPropertyCalls: builder =>
                            builder
                                .SetProperty(
                                    orderDetail => orderDetail.OrderStatusId,
                                    newOrderStatusId
                                )
                                .SetProperty(orderDetail => orderDetail.UpdatedAt, updatedAt)
                                .SetProperty(orderDetail => orderDetail.UpdatedBy, updatedBy)
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
