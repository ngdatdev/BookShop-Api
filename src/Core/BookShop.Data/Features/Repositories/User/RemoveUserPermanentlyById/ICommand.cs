using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.User.RemoveUserPermanentlyById;

/// <summary>
///     Interface for Command RemoveUserPermanentlyByIdRepository
/// </summary>
public partial interface IRemoveUserPermanentlyByIdRepository
{
    Task<bool> RemoveUserPermanentlyByIdCommandAsync(
        Guid userId,
        CancellationToken cancellationToken
    );
}
