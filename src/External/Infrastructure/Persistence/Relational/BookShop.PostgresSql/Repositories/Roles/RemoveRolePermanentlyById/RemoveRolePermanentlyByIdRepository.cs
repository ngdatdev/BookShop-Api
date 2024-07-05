using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.Roles.RemoveRolePermanentlyById;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Roles.RemoveRolePermanentlyById;

/// <summary>
///    Implement of IRemoveRolePermanentlyById repository.
/// </summary>
internal partial class RemoveRolePermanentlyByIdRepository : IRemoveRolePermanentlyByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<RoleDetail> _roleDetails;
    private readonly DbSet<Role> _roles;

    public RemoveRolePermanentlyByIdRepository(BookShopContext context)
    {
        _context = context;
        _roleDetails = _context.Set<RoleDetail>();
        _roles = _context.Set<Role>();
    }
}
