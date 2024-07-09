using BookShop.Data.Features.Repositories.User.RestoreUserById;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.User.RestoreUserById;

/// <summary>
///    Implement of IRestoreUserById repository.
/// </summary>
internal partial class RestoreUserByIdRepository : IRestoreUserByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<BookShop.Data.Shared.Entities.User> _users;
    private DbSet<BookShop.Data.Shared.Entities.UserDetail> _userDetails;

    public RestoreUserByIdRepository(BookShopContext context)
    {
        _context = context;
        _users = _context.Set<BookShop.Data.Shared.Entities.User>();
        _userDetails = _context.Set<BookShop.Data.Shared.Entities.UserDetail>();
    }
}
