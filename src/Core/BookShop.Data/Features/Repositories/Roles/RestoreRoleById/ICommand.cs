using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Roles.RestoreRoleById;

/// <summary>
///     Interface for Command RestoreRoleByIdRepository
/// </summary>
public partial interface IRestoreRoleByIdRepository
{
    Task<bool> RestoreRoleTemporarilyByIdCommandAsync(
        Guid roleId,
        CancellationToken cancellationToken
    );
}
