using BookShop.Data.Features.Repositories.Auth.ChangingPassword;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Auth.ChangingPassword;

/// <summary>
///    Implement of ChangingPassword Repository.
/// </summary>
internal partial class ChangingPasswordRepository : IChangingPasswordRepository
{
    private readonly BookShopContext _context;
    private DbSet<UserDetail> _usersDetail;
    private DbSet<UserToken> _userTokens;

    public ChangingPasswordRepository(BookShopContext context)
    {
        _context = context;
        _usersDetail = _context.Set<UserDetail>();
        _userTokens = _context.Set<UserToken>();
    }
}
