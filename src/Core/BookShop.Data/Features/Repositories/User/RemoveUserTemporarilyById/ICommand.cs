using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.User.RemoveUserTemporarilyById;

/// <summary>
///     Interface for Command RemoveUserTemporarilyById Repository.
/// </summary>
public partial interface IRemoveUserTemporarilyByIdRepository
{
    Task<bool> RemoveUserTemporarilyByIdCommandAsync(
        Guid userId,
        DateTime removedAt,
        Guid removedBy,
        CancellationToken cancellationToken
    );
}
