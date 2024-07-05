using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Roles.RestoreRoleById;

/// <summary>
///     Interface for Query RestoreRoleByIdRepository Repository
/// </summary>
public partial interface IRestoreRoleByIdRepository
{
    Task<bool> IsRoleTemporarilyRemovedByIdQueryAsync(
        Guid roleId,
        CancellationToken cancellationToken
    );

    Task<bool> IsRoleFoundByIdQueryAsync(Guid roleId, CancellationToken cancellationToken);
}
