using BookShop.Data.Shared.Entities;
using BookShop.Data.Shared.Repositories.VerifyAccessToken;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.wwwOther.VerifyDataAccess;

/// <summary>
///    This is an auto generated class.
/// </summary>
internal partial class VerifyAccessTokenRepository : IVerifyAccessTokenRepository
{
    private readonly BookShopContext _context;
    private readonly DbSet<RefreshToken> _refreshTokens;
    private readonly DbSet<UserDetail> _userDetails;

    public VerifyAccessTokenRepository(BookShopContext context)
    {
        _context = context;
        _refreshTokens = context.Set<RefreshToken>();
        _userDetails = context.Set<UserDetail>();
    }
}
