using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Roles.RemoveRoleTemporarilyById;

/// <summary>
///     Interface for Query RemoveRoleTemporarilyByIdRepository Repository
/// </summary>
public partial interface IRemoveRoleTemporarilyByIdRepository
{
    Task<bool> IsRoleTemporarilyRemovedByIdQueryAsync(
        Guid roleId,
        CancellationToken cancellationToken
    );

    Task<bool> IsRoleFoundByIdQueryAsync(Guid roleId, CancellationToken cancellationToken);
}
