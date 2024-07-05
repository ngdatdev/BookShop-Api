using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.PostgresSql.Constants;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Roles.UpdateRoleById;

/// <summary>
///    Implement of query IUpdateRoleById repository.
/// </summary>
internal partial class UpdateRoleByIdRepository
{
    public Task<bool> IsSameRoleNameFoundByRoleNameQueryAsync(
        string roleName,
        CancellationToken cancellationToken
    )
    {
        return _roles
            .AsNoTracking()
            .Where(predicate: role =>
                EF.Functions.Collate(role.Name, CommonConstant.DbCollation.CASE_INSENSITIVE)
                    .Equals(roleName)
            )
            .AnyAsync(cancellationToken: cancellationToken);
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

    public Task<bool> IsRoleFoundByIdQueryAsync(Guid roleId, CancellationToken cancellationToken)
    {
        return _roles
            .AsNoTracking()
            .AnyAsync(predicate: role => role.Id == roleId, cancellationToken: cancellationToken);
    }
}
