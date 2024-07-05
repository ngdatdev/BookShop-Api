using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Roles.RemoveRolePermanentlyById;

/// <summary>
///     Interface for Command RemoveRolePermanentlyByIdRepository
/// </summary>
public partial interface IRemoveRolePermanentlyByIdRepository
{
    Task<bool> DeleteRolePermanentlyByIdCommandAsync(
        Guid roleId,
        CancellationToken cancellationToken
    );
}
