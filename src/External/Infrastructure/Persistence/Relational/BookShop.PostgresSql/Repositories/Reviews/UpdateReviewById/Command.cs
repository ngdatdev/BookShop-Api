using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Reviews.UpdateReviewById;

/// <summary>
///    Implement of query IUpdateReviewById repository.
/// </summary>
internal partial class UpdateReviewByIdRepository
{
    public async Task<bool> UpdateReviewByIdCommandAsync(
        Guid reviewId,
        string comment,
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
                        .ExecuteUpdateAsync(setPropertyCalls: builder =>
                            builder
                                .SetProperty(review => review.Comment, comment)
                                .SetProperty(review => review.UpdatedAt, DateTime.UtcNow)
                                .SetProperty(review => review.UpdatedBy, userId)
                        );

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
