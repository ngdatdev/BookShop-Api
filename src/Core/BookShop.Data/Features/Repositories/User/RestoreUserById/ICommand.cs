using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.User.RestoreUserById;

/// <summary>
///     Interface for Command RestoreUserByIdRepository.
/// </summary>
public partial interface IRestoreUserByIdRepository
{
    Task<bool> RestoreUserByIdCommandAsync(Guid userId, CancellationToken cancellationToken);
}
