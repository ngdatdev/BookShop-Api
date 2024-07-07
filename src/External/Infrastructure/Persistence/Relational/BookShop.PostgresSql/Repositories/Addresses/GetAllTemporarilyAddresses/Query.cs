using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Addresses.GetAllTemporarilyAddresses;

/// <summary>
///    Implement of query IGetAllTemporarilyAddressesRepository repository.
/// </summary>
internal partial class GetAllTemporarilyAddressesRepository
{
    public async Task<IEnumerable<Address>> FindAllTemporarilyAddressesQueryAsync(
        CancellationToken cancellationToken
    )
    {
        return await _addresses
            .Where(address =>
                address.RemovedAt != CommonConstant.MIN_DATE_TIME
                && address.RemovedBy != CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            )
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
