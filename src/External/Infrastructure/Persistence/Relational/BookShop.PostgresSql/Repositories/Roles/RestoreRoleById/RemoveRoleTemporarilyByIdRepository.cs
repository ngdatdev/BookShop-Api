using BookShop.Data.Features.Repositories.Roles.RestoreRoleById;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Roles.RestoreRoleById;

/// <summary>
///    Implement of IRestoreRoleById repository.
/// </summary>
internal partial class RestoreRoleByIdRepository : IRestoreRoleByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<RoleDetail> _roleDetails;

    public RestoreRoleByIdRepository(BookShopContext context)
    {
        _context = context;
        _roleDetails = _context.Set<RoleDetail>();
    }
}
