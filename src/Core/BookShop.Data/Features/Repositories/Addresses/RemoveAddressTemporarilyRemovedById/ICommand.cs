using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Addresses.RemoveAddressTemporarilyRemovedById;

/// <summary>
///     Interface for Command RemoveAddressTemporarilyRemovedById Repository
/// </summary>
public partial interface IRemoveAddressTemporarilyRemovedByIdRepository
{
    Task<bool> RemoveAddressTemporarilyRemovedByIdCommandAsync(
        Guid addressId,
        DateTime removedAt,
        Guid removedBy,
        CancellationToken cancellationToken
    );
}
