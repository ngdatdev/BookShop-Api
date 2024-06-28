using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.User.RestoreUserById;

/// <summary>
///    Implement of command IRestoreUserByIdRepository.
/// </summary>
internal partial class RestoreUserByIdRepository
{
    public async Task<bool> RestoreUserByIdCommandAsync(
        Guid userId,
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
                    _userDetails
                        .Where(predicate: user => user.UserId == userId)
                        .ExecuteUpdate(setPropertyCalls: builder =>
                            builder
                                .SetProperty(user => user.RemovedAt, CommonConstant.MIN_DATE_TIME)
                                .SetProperty(
                                    user => user.RemovedBy,
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
