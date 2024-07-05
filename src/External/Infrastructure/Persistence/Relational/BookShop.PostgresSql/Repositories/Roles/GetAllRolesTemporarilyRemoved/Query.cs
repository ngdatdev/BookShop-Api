using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Roles.GetAllRolesTemporarilyRemoved;

/// <summary>
///    Implement of query IGetAllRolesTemporarilyRemoved repository.
/// </summary>
internal partial class GetAllRolesTemporarilyRemovedRepository
{
    public async Task<IEnumerable<Role>> FindAllRolesTemporarilyRemovedQueryAsync(
        CancellationToken cancellationToken
    )
    {
        return await _roleDetails
            .AsNoTracking()
            .Where(predicate: role =>
                role.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
                && role.RemovedAt != CommonConstant.MIN_DATE_TIME
            )
            .Select(selector: role => new Role() { Id = role.RoleId, Name = role.Role.Name, })
            .ToListAsync();
    }
}
