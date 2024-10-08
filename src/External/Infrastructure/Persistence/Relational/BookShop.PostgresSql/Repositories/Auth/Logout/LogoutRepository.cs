using BookShop.Data.Features.Repositories.Auth.Logout;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Auth.Logout;

/// <summary>
///    Implement of ILogout repository.
/// </summary>
internal partial class LogoutRepository : ILogoutRepository
{
    private readonly BookShopContext _context;
    private DbSet<RefreshToken> _refreshTokens;

    public LogoutRepository(BookShopContext context)
    {
        _context = context;
        _refreshTokens = _context.Set<RefreshToken>();
    }
}
