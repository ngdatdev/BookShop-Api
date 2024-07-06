using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Addresses.GetAllDistrictsByProvinceName;

/// <summary>
///    Implement of query IGetAllDistrictsByProvinceNameRepository repository.
/// </summary>
internal partial class GetAllDistrictsByProvinceNameRepository
{
    public async Task<IEnumerable<string>> FindAllDistrictsByProvinceNameQueryAsync(
        string provinceName,
        CancellationToken cancellationToken
    )
    {
        return await _addresses
            .Where(address =>
                EF.Functions.Collate(
                        address.Province,
                        Constants.CommonConstant.DbCollation.CASE_INSENSITIVE
                    )
                    .Equals(provinceName)
                && address.RemovedAt == CommonConstant.MIN_DATE_TIME
                && address.RemovedBy == CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            )
            .Select(address => address.District)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
