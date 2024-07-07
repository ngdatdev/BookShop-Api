using BookShop.Data.Features.Repositories.Reviews;
using BookShop.Data.Features.Repositories.Reviews.AddReviewWithUserAndProductId;
using BookShop.Data.Features.Repositories.Reviews.RemoveReviewById;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.Reviews.AddReviewWithUserAndProductId;
using BookShop.PostgresSql.Repositories.Reviews.RemoveReviewById;

namespace BookShop.PostgresSql.Repositories.Reviews;

/// <summary>
///    Implement of ReviewFeatureRepository interface.
/// </summary>
internal class ReviewFeatureRepository : IReviewFeatureRepository
{
    private readonly BookShopContext _context;
    private IAddReviewWithUserAndProductIdRepository _addReviewWithUserAndProductIdRepository;
    private IRemoveReviewByIdRepository _removeReviewByIdRepository;

    internal ReviewFeatureRepository(BookShopContext context)
    {
        _context = context;
    }

    public IAddReviewWithUserAndProductIdRepository AddReviewWithUserAndProductIdRepository
    {
        get
        {
            return _addReviewWithUserAndProductIdRepository ??=
                new AddReviewWithUserAndProductIdRepository(context: _context);
        }
    }

    public IRemoveReviewByIdRepository RemoveReviewByIdRepository
    {
        get
        {
            return _removeReviewByIdRepository ??= new RemoveReviewByIdRepository(
                context: _context
            );
        }
    }
}
