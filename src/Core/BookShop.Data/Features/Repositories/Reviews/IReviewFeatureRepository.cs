using BookShop.Data.Features.Repositories.Reviews.AddReviewWithUserAndProductId;
using BookShop.Data.Features.Repositories.Reviews.RemoveReviewById;

namespace BookShop.Data.Features.Repositories.Reviews;

/// <summary>
///     Interface for review repository manager.
/// </summary>
public interface IReviewFeatureRepository
{
    /// <summary>
    ///     Gets add review with user id and product id feature repository.
    /// </summary>
    public IAddReviewWithUserAndProductIdRepository AddReviewWithUserAndProductIdRepository { get; }

    /// <summary>
    ///     Gets remove review by user id and review id feature repository.
    /// </summary>
    public IRemoveReviewByIdRepository RemoveReviewByIdRepository { get; }
}
