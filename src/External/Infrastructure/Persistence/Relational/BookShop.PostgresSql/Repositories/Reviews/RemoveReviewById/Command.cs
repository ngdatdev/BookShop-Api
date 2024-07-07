using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Reviews.RemoveReviewById;

/// <summary>
///    Implement of query IRemoveReviewById repository.
/// </summary>
internal partial class RemoveReviewByIdRepository
{
    public async Task<bool> RemoveReviewByIdCommandAsync(
        Guid reviewId,
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
                    await _reviews
                        .Where(predicate: review =>
                            review.Id == reviewId && review.UserId == userId
                        )
                        .ExecuteDeleteAsync(cancellationToken: cancellationToken);

                    dbTransactionResult = true;
                    await dbTransaction.CommitAsync(cancellationToken: cancellationToken);
                }
                catch
                {
                    await dbTransaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });

        return dbTransactionResult;
    }
}
