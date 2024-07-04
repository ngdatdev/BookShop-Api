using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Orders.RestoreOrderById;

/// <summary>
///    Implement of command IRestoreOrderByIdRepository.
/// </summary>
internal partial class RestoreOrderByIdRepository
{
    public async Task<bool> RestoreOrderByIdCommandAsync(
        Guid orderId,
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
                    _orders
                        .Where(predicate: order => order.Id == orderId)
                        .ExecuteUpdate(setPropertyCalls: builder =>
                            builder
                                .SetProperty(
                                    order => order.RemovedAt,
                                    Application.Shared.Common.CommonConstant.MIN_DATE_TIME
                                )
                                .SetProperty(
                                    order => order.RemovedBy,
                                    Application
                                        .Shared
                                        .Common
                                        .CommonConstant
                                        .DEFAULT_ENTITY_ID_AS_GUID
                                )
                        );

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
