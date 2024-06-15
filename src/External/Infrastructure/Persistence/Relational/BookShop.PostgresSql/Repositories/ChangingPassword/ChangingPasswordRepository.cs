using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.ChangingPassword;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.ChangingPassword;

/// <summary>
///    Implement of forgot password repository.
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
