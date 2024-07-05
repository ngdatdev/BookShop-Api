using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Roles.RemoveRoleTemporarilyById;

/// <summary>
///     Interface for Command RemoveRoleTemporarilyByIdRepository
/// </summary>
public partial interface IRemoveRoleTemporarilyByIdRepository
{
    Task<bool> DeleteRoleTemporarilyByIdCommandAsync(
        Guid roleId,
        Guid removedBy,
        DateTime removedAt,
        CancellationToken cancellationToken
    );
}
