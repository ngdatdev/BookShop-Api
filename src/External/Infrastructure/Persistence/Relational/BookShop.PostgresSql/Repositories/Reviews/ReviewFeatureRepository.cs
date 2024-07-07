using BookShop.Data.Features.Repositories.Reviews;
using BookShop.Data.Features.Repositories.Reviews.AddReviewWithUserAndProductId;
using BookShop.Data.Features.Repositories.Reviews.GetReviewsByProductId;
using BookShop.Data.Features.Repositories.Reviews.GetReviewsByUserId;
using BookShop.Data.Features.Repositories.Reviews.RemoveReviewById;
using BookShop.Data.Features.Repositories.Reviews.UpdateReviewById;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.Reviews.AddReviewWithUserAndProductId;
using BookShop.PostgresSql.Repositories.Reviews.GetReviewsByProductId;
using BookShop.PostgresSql.Repositories.Reviews.GetReviewsByUserId;
using BookShop.PostgresSql.Repositories.Reviews.RemoveReviewById;
using BookShop.PostgresSql.Repositories.Reviews.UpdateReviewById;

namespace BookShop.PostgresSql.Repositories.Reviews;

/// <summary>
///    Implement of ReviewFeatureRepository interface.
/// </summary>
internal class ReviewFeatureRepository : IReviewFeatureRepository
{
    private readonly BookShopContext _context;
    private IAddReviewWithUserAndProductIdRepository _addReviewWithUserAndProductIdRepository;
    private IRemoveReviewByIdRepository _removeReviewByIdRepository;
    private IUpdateReviewByIdRepository _updateReviewByIdRepository;
    private IGetReviewsByProductIdRepository _getReviewsByProductIdRepository;
    private IGetReviewsByUserIdRepository _getReviewsByUserIdRepository;

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

    public IUpdateReviewByIdRepository UpdateReviewByIdRepository
    {
        get
        {
            return _updateReviewByIdRepository ??= new UpdateReviewByIdRepository(
                context: _context
            );
        }
    }

    public IGetReviewsByProductIdRepository GetReviewsByProductIdRepository
    {
        get
        {
            return _getReviewsByProductIdRepository ??= new GetReviewsByProductIdRepository(
                context: _context
            );
        }
    }

    public IGetReviewsByUserIdRepository GetReviewsByUserIdRepository
    {
        get
        {
            return _getReviewsByUserIdRepository ??= new GetReviewsByUserIdRepository(
                context: _context
            );
        }
    }
}
