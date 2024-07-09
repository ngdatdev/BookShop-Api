using BookShop.Data.Features.Repositories.Reviews.GetReviewsByUserId;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Reviews.GetReviewsByUserId;

/// <summary>
///    Implement of IGetReviewsByUserId repository.
/// </summary>
internal partial class GetReviewsByUserIdRepository : IGetReviewsByUserIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<Review> _reviews;

    public GetReviewsByUserIdRepository(BookShopContext context)
    {
        _context = context;
        _reviews = _context.Set<Review>();
    }
}
