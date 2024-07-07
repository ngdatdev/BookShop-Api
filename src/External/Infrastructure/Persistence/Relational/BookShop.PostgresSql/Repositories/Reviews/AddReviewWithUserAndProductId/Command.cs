using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Reviews.AddReviewWithUserAndProductId;

/// <summary>
///    Implement of query IAddReviewWithUserAndProductId repository.
/// </summary>
internal partial class AddReviewWithUserAndProductIdRepository
{
    public async Task<bool> AddReviewWithUserAndProductIdCommandAsync(
        Review newReview,
        CancellationToken cancellationToken
    )
    {
        using var dbTransaction = await _context.Database.BeginTransactionAsync(
            cancellationToken: cancellationToken
        );

        try
        {
            await _reviews.AddAsync(entity: newReview, cancellationToken: cancellationToken);

            await _context.SaveChangesAsync(cancellationToken: cancellationToken);

            await dbTransaction.CommitAsync(cancellationToken: cancellationToken);
        }
        catch
        {
            return false;
        }

        return true;
    }
}
