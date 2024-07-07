using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.Reviews.UpdateReviewById;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Reviews.UpdateReviewById;

/// <summary>
///    Implement of IUpdateReviewById repository.
/// </summary>
internal partial class UpdateReviewByIdRepository : IUpdateReviewByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<Review> _reviews;

    public UpdateReviewByIdRepository(BookShopContext context)
    {
        _context = context;
        _reviews = _context.Set<Review>();
    }
}
