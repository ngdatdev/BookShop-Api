using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Addresses.GetAllDistrictsByProvinceName;

/// <summary>
///     Interface for Query GetAllDistrictsByProvinceNameRepository Repository
/// </summary>
public partial interface IGetAllDistrictsByProvinceNameRepository
{
    Task<IEnumerable<string>> FindAllDistrictsByProvinceNameQueryAsync(
        string provinceName,
        CancellationToken cancellationToken
    );
}
