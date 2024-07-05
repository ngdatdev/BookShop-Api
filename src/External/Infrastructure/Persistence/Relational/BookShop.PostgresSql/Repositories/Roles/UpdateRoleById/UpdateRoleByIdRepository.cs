using BookShop.Data.Features.Repositories.Roles.UpdateRoleById;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Roles.UpdateRoleById;

/// <summary>
///    Implement of IUpdateRoleById repository.
/// </summary>
internal partial class UpdateRoleByIdRepository : IUpdateRoleByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<RoleDetail> _roleDetails;
    private DbSet<Role> _roles;

    public UpdateRoleByIdRepository(BookShopContext context)
    {
        _context = context;
        _roleDetails = _context.Set<RoleDetail>();
        _roles = _context.Set<Role>();
    }
}
