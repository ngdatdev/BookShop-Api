using BookShop.Data.Features.Repositories.Roles.GetAllRolesTemporarilyRemoved;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Roles.GetAllRolesTemporarilyRemoved;

/// <summary>
///    Implement of IGetAllRolesTemporarilyRemoved repository.
/// </summary>
internal partial class GetAllRolesTemporarilyRemovedRepository
    : IGetAllRolesTemporarilyRemovedRepository
{
    private readonly BookShopContext _context;
    private DbSet<RoleDetail> _roleDetails;

    public GetAllRolesTemporarilyRemovedRepository(BookShopContext context)
    {
        _context = context;
        _roleDetails = _context.Set<RoleDetail>();
    }
}
