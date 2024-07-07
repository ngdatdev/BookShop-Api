using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Addresses.UpdateAddressById;

/// <summary>
///    Implement of query IUpdateAddressByIdRepository repository.
/// </summary>
internal partial class UpdateAddressByIdRepository
{
    public Task<Address> FindAddressByIdQueryAsync(
        Guid addressId,
        CancellationToken cancellationToken
    )
    {
        return _addresses
            .AsNoTracking()
            .Where(predicate: address => address.Id == addressId)
            .FirstOrDefaultAsync();
    }

    public Task<bool> IsAddressTemporarilyRemovedByIdQueryAsync(
        Guid addressId,
        CancellationToken cancellationToken
    )
    {
        return _addresses
            .AsNoTracking()
            .AnyAsync(
                predicate: address =>
                    address.Id == addressId
                    && address.RemovedAt != CommonConstant.MIN_DATE_TIME
                    && address.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                cancellationToken: cancellationToken
            );
    }
}
