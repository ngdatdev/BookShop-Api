using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.Reviews.AddReviewWithUserAndProductId;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Reviews.AddReviewWithUserAndProductId;

/// <summary>
///    Implement of IAddReviewWithUserAndProductId repository.
/// </summary>
internal partial class AddReviewWithUserAndProductIdRepository
    : IAddReviewWithUserAndProductIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<Review> _reviews;
    private DbSet<BookShop.Data.Shared.Entities.Product> _products;

    public AddReviewWithUserAndProductIdRepository(BookShopContext context)
    {
        _context = context;
        _reviews = _context.Set<Review>();
        _products = _context.Set<BookShop.Data.Shared.Entities.Product>();
    }
}
