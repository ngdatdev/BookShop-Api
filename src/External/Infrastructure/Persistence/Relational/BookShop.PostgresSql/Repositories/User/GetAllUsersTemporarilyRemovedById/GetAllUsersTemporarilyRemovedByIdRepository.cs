using BookShop.Data.Features.Repositories.User.GetAllUsersTemporarilyRemovedById;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.User.GetAllUsersTemporarilyRemovedById;

/// <summary>
///    Implement of IGetAllUsersTemporarilyRemovedById repository.
/// </summary>
internal partial class GetAllUsersTemporarilyRemovedByIdRepository
    : IGetAllUsersTemporarilyRemovedByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<UserDetail> _usersDetails;

    public GetAllUsersTemporarilyRemovedByIdRepository(BookShopContext context)
    {
        _context = context;
        _usersDetails = _context.Set<UserDetail>();
    }
}
