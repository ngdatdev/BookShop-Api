using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Roles.UpdateRoleById;

/// <summary>
///     Interface for Query UpdateRoleByIdRepository Repository
/// </summary>
public partial interface IUpdateRoleByIdRepository
{
    Task<bool> IsRoleTemporarilyRemovedByIdQueryAsync(
        Guid roleId,
        CancellationToken cancellationToken
    );

    Task<bool> IsRoleFoundByIdQueryAsync(Guid roleId, CancellationToken cancellationToken);

    Task<bool> IsSameRoleNameFoundByRoleNameQueryAsync(
        string roleName,
        CancellationToken cancellationToken
    );
}
