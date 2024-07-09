using BookShop.Data.Features.Repositories.Auth.RegisterAsUser;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Auth.RegisterAsUser;

/// <summary>
///    Implement of IRegisterAsUser repository.
/// </summary>
internal partial class RegisterAsUserRepository : IRegisterAsUserRepository
{
    private readonly BookShopContext _context;
    private DbSet<BookShop.Data.Shared.Entities.User> _users;
    private DbSet<Cart> _carts;

    public RegisterAsUserRepository(BookShopContext context)
    {
        _context = context;
        _users = _context.Set<BookShop.Data.Shared.Entities.User>();
        _carts = _context.Set<Cart>();
    }
}
