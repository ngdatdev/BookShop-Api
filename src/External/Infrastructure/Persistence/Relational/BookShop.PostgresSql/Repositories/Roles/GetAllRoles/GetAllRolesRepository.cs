using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.Roles.GetAllRoles;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Roles.GetAllRoles;

/// <summary>
///    Implement of IGetAllRoles repository.
/// </summary>
internal partial class GetAllRolesRepository : IGetAllRolesRepository
{
    private readonly BookShopContext _context;
    private DbSet<RoleDetail> _roleDetails;

    public GetAllRolesRepository(BookShopContext context)
    {
        _context = context;
        _roleDetails = _context.Set<RoleDetail>();
    }
}
