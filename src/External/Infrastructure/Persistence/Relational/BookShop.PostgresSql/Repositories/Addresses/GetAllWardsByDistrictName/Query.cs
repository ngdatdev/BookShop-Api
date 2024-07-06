using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Addresses.GetAllWardsByDistrictName;

/// <summary>
///    Implement of query IGetAllWardsByDistrictNameRepository repository.
/// </summary>
internal partial class GetAllWardsByDistrictNameRepository
{
    public async Task<IEnumerable<string>> FindAllWardsByDistrictNameQueryAsync(
        string districtName,
        CancellationToken cancellationToken
    )
    {
        return await _addresses
            .Where(address =>
                EF.Functions.Collate(
                        address.District,
                        Constants.CommonConstant.DbCollation.CASE_INSENSITIVE
                    )
                    .Equals(districtName)
                && address.RemovedAt == CommonConstant.MIN_DATE_TIME
                && address.RemovedBy == CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            )
            .Select(address => address.Ward)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
