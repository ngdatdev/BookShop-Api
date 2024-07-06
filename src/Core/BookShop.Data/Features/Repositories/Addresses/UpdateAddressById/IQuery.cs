using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Addresses.UpdateAddressById;

/// <summary>
///     Interface for Query UpdateAddressById Repository
/// </summary>
public partial interface IUpdateAddressByIdRepository
{
    Task<Shared.Entities.Address> FindAddressByIdQueryAsync(
        Guid addressId,
        CancellationToken cancellationToken
    );

    Task<bool> IsAddressTemporarilyRemovedByIdQueryAsync(
        Guid addressId,
        CancellationToken cancellationToken
    );
}
