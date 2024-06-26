using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.User.RemoveUserPermanentlyById;

/// <summary>
///     Interface for Query RemoveUserPermanentlyByIdRepository
/// </summary>
public partial interface IRemoveUserPermanentlyByIdRepository
{
    Task<string> FindAvatarUserByIdQueryAsync(Guid userId, CancellationToken cancellationToken);

    Task<bool> IsUserTemporarilyRemovedByIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    );
}
