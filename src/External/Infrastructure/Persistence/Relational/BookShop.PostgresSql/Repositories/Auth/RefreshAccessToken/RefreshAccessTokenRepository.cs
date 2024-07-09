using BookShop.Data.Features.Repositories.Auth.RefreshAccessToken;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Auth.RefreshAccessToken;

/// <summary>
///    Implement of IRefreshAccessToken repository.
/// </summary>
internal partial class RefreshAccessTokenRepository : IRefreshAccessTokenRepository
{
    private readonly BookShopContext _context;
    private DbSet<RefreshToken> _refreshTokens;

    public RefreshAccessTokenRepository(BookShopContext context)
    {
        _context = context;
        _refreshTokens = _context.Set<RefreshToken>();
    }
}
