using BookShop.Data.Features.Repositories.User.GetAllUsers;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.User.GetAllUsers;

/// <summary>
///    Implement of IGetAllUsersRepository repository.
/// </summary>
internal partial class GetAllUsersRepository : IGetAllUsersRepository
{
    private readonly BookShopContext _context;
    private DbSet<UserDetail> _usersDetails;

    public GetAllUsersRepository(BookShopContext context)
    {
        _context = context;
        _usersDetails = _context.Set<UserDetail>();
    }
}
