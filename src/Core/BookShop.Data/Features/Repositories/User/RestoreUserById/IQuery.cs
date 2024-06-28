using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.User.RestoreUserById;

/// <summary>
///     Interface for Query RestoreUserByIdRepository.
/// </summary>
public partial interface IRestoreUserByIdRepository
{
    Task<bool> IsUserFoundByIdQueryAsync(Guid userId, CancellationToken cancellationToken);

    Task<bool> IsUserTemporarilyRemovedByIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    );
}
