using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Addresses.RemoveAddressTemporarilyRemovedById;

/// <summary>
///     Interface for Query RemoveAddressTemporarilyRemovedById Repository
/// </summary>
public partial interface IRemoveAddressTemporarilyRemovedByIdRepository
{
    Task<bool> IsAddressFoundByIdQueryAsync(Guid addressId, CancellationToken cancellationToken);

    Task<bool> IsAddressTemporarilyRemovedByIdQueryAsync(
        Guid addressId,
        CancellationToken cancellationToken
    );
}
