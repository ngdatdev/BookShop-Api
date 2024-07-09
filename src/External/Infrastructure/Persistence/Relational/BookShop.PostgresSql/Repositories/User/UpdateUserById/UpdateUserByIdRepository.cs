using BookShop.Data.Features.Repositories.User.UpdateUserById;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.User.UpdateUserById;

/// <summary>
///    Implement of IUpdateUserById repository.
/// </summary>
internal partial class UpdateUserByIdRepository : IUpdateUserByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<BookShop.Data.Shared.Entities.User> _users;
    private DbSet<UserDetail> _userDetails;
    private DbSet<Address> _addresses;

    public UpdateUserByIdRepository(BookShopContext context)
    {
        _context = context;
        _users = _context.Set<BookShop.Data.Shared.Entities.User>();
        _userDetails = _context.Set<UserDetail>();
        _addresses = _context.Set<Address>();
    }
}
