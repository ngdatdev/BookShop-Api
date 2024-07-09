using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Product.RestoreProductById;

/// <summary>
///    Implement of command IRestoreProductById repository.
/// </summary>
internal partial class RestoreProductByIdRepository
{
    public async Task<bool> RestoreProductByIdCommandAsync(
        Guid productId,
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
                    _products
                        .Where(predicate: product => product.Id == productId)
                        .ExecuteUpdate(setPropertyCalls: builder =>
                            builder
                                .SetProperty(
                                    product => product.RemovedAt,
                                    CommonConstant.MIN_DATE_TIME
                                )
                                .SetProperty(
                                    product => product.RemovedBy,
                                    CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
                                )
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
