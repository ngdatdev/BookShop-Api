using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.Roles.RemoveRoleTemporarilyById;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Roles.RemoveRoleTemporarilyById;

/// <summary>
///    Implement of IRemoveRoleTemporarilyById repository.
/// </summary>
internal partial class RemoveRoleTemporarilyByIdRepository : IRemoveRoleTemporarilyByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<RoleDetail> _roleDetails;

    public RemoveRoleTemporarilyByIdRepository(BookShopContext context)
    {
        _context = context;
        _roleDetails = _context.Set<RoleDetail>();
    }
}
