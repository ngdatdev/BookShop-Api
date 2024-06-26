using BookShop.Data.Features.Repositories.Auth.ForgotPassword;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Auth.ForgotPassword;

/// <summary>
///    Implement of forgot password repository.
/// </summary>
internal partial class ForgotPasswordRepository : IForgotPasswordRepository
{
    private readonly BookShopContext _context;
    private DbSet<UserDetail> _usersDetail;
    private DbSet<UserToken> _userTokens;

    public ForgotPasswordRepository(BookShopContext context)
    {
        _context = context;
        _usersDetail = _context.Set<UserDetail>();
        _userTokens = _context.Set<UserToken>();
    }
}
