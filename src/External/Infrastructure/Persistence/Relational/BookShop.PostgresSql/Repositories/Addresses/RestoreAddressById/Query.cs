using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Addresses.RestoreAddressById;

/// <summary>
///    Implement of query IRestoreAddressByIdRepository repository.
/// </summary>
internal partial class RestoreAddressByIdRepository
{
    public Task<bool> IsAddressFoundByIdQueryAsync(
        Guid addressId,
        CancellationToken cancellationToken
    )
    {
        return _addresses
            .AsNoTracking()
            .AnyAsync(
                predicate: address => address.Id == addressId,
                cancellationToken: cancellationToken
            );
        ;
    }

    public Task<bool> IsAddressTemporarilyRemovedByIdQueryAsync(
        Guid addressId,
        CancellationToken cancellationToken
    )
    {
        return _addresses.AnyAsync(
            predicate: address =>
                address.Id == addressId
                && address.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
                && address.RemovedAt != CommonConstant.MIN_DATE_TIME,
            cancellationToken: cancellationToken
        );
    }
}
