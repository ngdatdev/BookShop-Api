using BookShop.Data.Shared.Entities;
using BookShop.Data.Shared.Repositories.VerifyAccessToken;
using BookShop.SqlServer.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.SqlServer.Repositories.VerifyDataAccess;

/// <summary>
///    This is an auto generated class.
/// </summary>
internal partial class VerifyDataAccessRepository : IVerifyAccessTokenRepository
{
    private readonly BookShopContext _context;
    private readonly DbSet<RefreshToken> _refreshTokens;
    private readonly DbSet<UserDetail> _userDetails;

    public VerifyDataAccessRepository(BookShopContext context)
    {
        _context = context;
        _refreshTokens = context.Set<RefreshToken>();
        _userDetails = context.Set<UserDetail>();
    }

}
