using BookShop.Data.Features.Repositories.Auth.Login;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Auth.Login;

/// <summary>
///    Implement of ILogin repository.
/// </summary>
internal partial class LoginRepository : ILoginRepository
{
    private readonly BookShopContext _context;
    private DbSet<UserDetail> _usersDetail;
    private DbSet<RefreshToken> _refreshTokens;

    public LoginRepository(BookShopContext context)
    {
        _context = context;
        _usersDetail = _context.Set<UserDetail>();
        _refreshTokens = _context.Set<RefreshToken>();
    }
}
