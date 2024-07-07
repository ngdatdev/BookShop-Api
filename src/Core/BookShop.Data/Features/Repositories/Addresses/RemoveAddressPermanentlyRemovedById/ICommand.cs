using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Addresses.RemoveAddressPermanentlyRemovedById;

/// <summary>
///     Interface for Command RemoveAddressPermanentlyRemovedById Repository
/// </summary>
public partial interface IRemoveAddressPermanentlyRemovedByIdRepository
{
    Task<bool> RemoveAddressPermanentlyRemovedByIdCommandAsync(
        Guid addressId,
        CancellationToken cancellationToken
    );
}
