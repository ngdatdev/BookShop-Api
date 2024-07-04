using BookShop.Data.Features.Repositories.Roles.CreateRole;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Roles.CreateRole;

/// <summary>
///    Implement of ICreateRole repository.
/// </summary>
internal partial class CreateRoleRepository : ICreateRoleRepository
{
    private readonly BookShopContext _context;
    private DbSet<Role> _roles;
    private DbSet<RoleDetail> _roleDetails;

    public CreateRoleRepository(BookShopContext context)
    {
        _context = context;
        _roles = _context.Set<Role>();
        _roleDetails = _context.Set<RoleDetail>();
    }
}
