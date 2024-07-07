using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.Reviews.RemoveReviewById;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Reviews.RemoveReviewById;

/// <summary>
///    Implement of IRemoveReviewById repository.
/// </summary>
internal partial class RemoveReviewByIdRepository : IRemoveReviewByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<Review> _reviews;

    public RemoveReviewByIdRepository(BookShopContext context)
    {
        _context = context;
        _reviews = _context.Set<Review>();
    }
}
