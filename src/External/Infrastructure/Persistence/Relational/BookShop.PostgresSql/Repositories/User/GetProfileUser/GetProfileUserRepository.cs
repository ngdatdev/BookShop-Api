using BookShop.Data.Features.Repositories.User.GetProfileUser;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.User.GetProfileUser;

/// <summary>
///    Implement of IGetProfileUser repository.
/// </summary>
internal partial class GetProfileUserRepository : IGetProfileUserRepository
{
    private readonly BookShopContext _context;
    private DbSet<UserDetail> _usersDetails;

    public GetProfileUserRepository(BookShopContext context)
    {
        _context = context;
        _usersDetails = _context.Set<UserDetail>();
    }
}
