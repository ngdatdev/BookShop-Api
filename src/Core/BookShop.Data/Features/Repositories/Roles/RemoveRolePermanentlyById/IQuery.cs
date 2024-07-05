using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Roles.RemoveRolePermanentlyById;

/// <summary>
///     Interface for Query RemoveRolePermanentlyByIdRepository Repository
/// </summary>
public partial interface IRemoveRolePermanentlyByIdRepository
{
    Task<bool> IsRoleTemporarilyRemovedByIdQueryAsync(
        Guid roleId,
        CancellationToken cancellationToken
    );

    Task<bool> IsRoleFoundByIdQueryAsync(Guid roleId, CancellationToken cancellationToken);
}
