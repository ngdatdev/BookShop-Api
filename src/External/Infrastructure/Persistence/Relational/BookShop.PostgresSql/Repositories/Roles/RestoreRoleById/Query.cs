using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Roles.RestoreRoleById;

/// <summary>
///    Implement of query IRestoreRoleById repository.
/// </summary>
internal partial class RestoreRoleByIdRepository
{
    public Task<bool> IsRoleFoundByIdQueryAsync(Guid roleId, CancellationToken cancellationToken)
    {
        return _roleDetails
            .AsNoTracking()
            .AnyAsync(
                predicate: roleDetail => roleDetail.RoleId == roleId,
                cancellationToken: cancellationToken
            );
    }

    public Task<bool> IsRoleTemporarilyRemovedByIdQueryAsync(
        Guid roleId,
        CancellationToken cancellationToken
    )
    {
        return _roleDetails
            .AsNoTracking()
            .AnyAsync(
                predicate: roleDetail =>
                    roleDetail.RoleId == roleId
                    && roleDetail.RemovedAt
                        != Application.Shared.Common.CommonConstant.MIN_DATE_TIME
                    && roleDetail.RemovedBy
                        != Application.Shared.Common.CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                cancellationToken: cancellationToken
            );
    }
}
