using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.User.RemoveUserTemporarilyById;

/// <summary>
///     Interface for Query RemoveUserTemporarilyById Repository.
/// </summary>
public partial interface IRemoveUserTemporarilyByIdRepository
{
    Task<bool> IsUserFoundByIdQueryAsync(Guid userId, CancellationToken cancellationToken);

    Task<bool> IsUserTemporarilyRemovedByIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    );
}
