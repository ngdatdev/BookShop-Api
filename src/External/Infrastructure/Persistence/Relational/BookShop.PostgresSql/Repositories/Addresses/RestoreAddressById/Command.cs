using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Addresses.RestoreAddressById;

/// <summary>
///    Implement of command IRestoreAddressById repository.
/// </summary>
internal partial class RestoreAddressByIdRepository
{
    public async Task<bool> RestoreAddressByIdCommandAsync(
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
                        .Where(predicate: address => address.Id == addressId)
                        .ExecuteUpdateAsync(setPropertyCalls: builder =>
                            builder
                                .SetProperty(
                                    address => address.RemovedAt,
                                    CommonConstant.MIN_DATE_TIME
                                )
                                .SetProperty(
                                    address => address.RemovedBy,
                                    CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
                                )
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
