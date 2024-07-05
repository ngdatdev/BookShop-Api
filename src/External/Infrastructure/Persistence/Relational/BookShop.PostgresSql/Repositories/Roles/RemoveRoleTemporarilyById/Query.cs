using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.PostgresSql.Constants;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Roles.RemoveRoleTemporarilyById;

/// <summary>
///    Implement of query IRemoveRoleTemporarilyById repository.
/// </summary>
internal partial class RemoveRoleTemporarilyByIdRepository
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
