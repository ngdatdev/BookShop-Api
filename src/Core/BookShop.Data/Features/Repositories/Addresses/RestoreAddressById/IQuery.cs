using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Addresses.RestoreAddressById;

/// <summary>
///     Interface for Query RestoreAddressById Repository
/// </summary>
public partial interface IRestoreAddressByIdRepository
{
    Task<bool> IsAddressFoundByIdQueryAsync(Guid addressId, CancellationToken cancellationToken);

    Task<bool> IsAddressTemporarilyRemovedByIdQueryAsync(
        Guid addressId,
        CancellationToken cancellationToken
    );
}
