using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Addresses.RemoveAddressPermanentlyRemovedById;

/// <summary>
///     Interface for Query RemoveAddressPermanentlyRemovedById Repository
/// </summary>
public partial interface IRemoveAddressPermanentlyRemovedByIdRepository
{
    Task<bool> IsAddressFoundByIdQueryAsync(Guid addressId, CancellationToken cancellationToken);

    Task<bool> IsAddressTemporarilyRemovedByIdQueryAsync(
        Guid addressId,
        CancellationToken cancellationToken
    );
}
