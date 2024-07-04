using System.Threading;
using System.Threading.Tasks;
using BookShop.PostgresSql.Constants;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Roles.CreateRole;

/// <summary>
///    Implement of query ICreateRole repository.
/// </summary>
internal partial class CreateRoleRepository
{
    public Task<bool> IsSameRoleNameFoundByRoleNameQueryAsync(
        string newRoleName,
        CancellationToken cancellationToken
    )
    {
        return _roles
            .AsNoTracking()
            .AnyAsync(
                predicate: role =>
                    EF.Functions.Collate(role.Name, CommonConstant.DbCollation.CASE_INSENSITIVE)
                        .Equals(newRoleName),
                cancellationToken: cancellationToken
            );
    }

    public Task<bool> IsRoleTemporarilyRemovedByRoleNameQueryAsync(
        string newRoleName,
        CancellationToken cancellationToken
    )
    {
        return _roleDetails
            .AsNoTracking()
            .AnyAsync(
                predicate: role =>
                    role.RemovedBy
                        != BookShop
                            .Application
                            .Shared
                            .Common
                            .CommonConstant
                            .DEFAULT_ENTITY_ID_AS_GUID
                    && role.RemovedAt
                        != BookShop.Application.Shared.Common.CommonConstant.MIN_DATE_TIME,
                cancellationToken: cancellationToken
            );
    }
}
