using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.Reviews.GetReviewsByProductId;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Reviews.GetReviewsByProductId;

/// <summary>
///    Implement of IGetReviewsByProductId repository.
/// </summary>
internal partial class GetReviewsByProductIdRepository : IGetReviewsByProductIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<Review> _reviews;

    public GetReviewsByProductIdRepository(BookShopContext context)
    {
        _context = context;
        _reviews = _context.Set<Review>();
    }
}
