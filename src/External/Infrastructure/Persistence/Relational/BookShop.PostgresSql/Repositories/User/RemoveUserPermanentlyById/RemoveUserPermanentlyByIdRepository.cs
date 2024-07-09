using BookShop.Data.Features.Repositories.User.RemoveUserPermanentlyById;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.User.RemoveUserPermanentlyById;

/// <summary>
///    Implement of IRemoveUserPermanentlyById repository.
/// </summary>
internal partial class RemoveUserPermanentlyByIdRepository : IRemoveUserPermanentlyByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<BookShop.Data.Shared.Entities.User> _users;
    private DbSet<UserDetail> _userDetails;
    private DbSet<Review> _reviews;
    private DbSet<RefreshToken> _refreshTokens;
    private DbSet<Cart> _carts;
    private DbSet<Order> _orders;

    public RemoveUserPermanentlyByIdRepository(BookShopContext context)
    {
        _context = context;
        _users = _context.Set<BookShop.Data.Shared.Entities.User>();
        _userDetails = _context.Set<UserDetail>();
        _carts = _context.Set<Cart>();
        _orders = _context.Set<Order>();
        _refreshTokens = _context.Set<RefreshToken>();
        _reviews = _context.Set<Review>();
    }
}
