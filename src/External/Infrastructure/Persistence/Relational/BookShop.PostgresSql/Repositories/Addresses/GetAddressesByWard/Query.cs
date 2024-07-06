using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Addresses.GetAddressesByWard;

/// <summary>
///    Implement of query IGetAddressesByWardRepository repository.
/// </summary>
internal partial class GetAddressesByWardRepository
{
    public async Task<IEnumerable<Address>> FindAllAddressesByWardNameQueryAsync(
        string ward,
        CancellationToken cancellationToken
    )
    {
        return await _addresses
            .Where(address =>
                EF.Functions.Collate(
                        address.Ward,
                        Constants.CommonConstant.DbCollation.CASE_INSENSITIVE
                    )
                    .Equals(ward)
                && address.RemovedAt == CommonConstant.MIN_DATE_TIME
                && address.RemovedBy == CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            )
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
