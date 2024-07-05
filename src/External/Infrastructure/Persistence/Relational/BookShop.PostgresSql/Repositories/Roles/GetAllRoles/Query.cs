using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Roles.GetAllRoles;

/// <summary>
///    Implement of query IGetAllRoles repository.
/// </summary>
internal partial class GetAllRolesRepository
{
    public async Task<IEnumerable<Role>> FindAllRolesQueryAsync(CancellationToken cancellationToken)
    {
        return await _roleDetails
            .AsNoTracking()
            .Where(predicate: roleDetail =>
                roleDetail.RemovedAt == Application.Shared.Common.CommonConstant.MIN_DATE_TIME
                && roleDetail.RemovedBy
                    == Application.Shared.Common.CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            )
            .Select(roleDetail => new Role()
            {
                Id = roleDetail.RoleId,
                Name = roleDetail.Role.Name
            })
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
