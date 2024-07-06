using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Addresses.GetAllWardsByDistrictName;

/// <summary>
///     Interface for Query GetAllWardsByDistrictNameRepository Repository
/// </summary>
public partial interface IGetAllWardsByDistrictNameRepository
{
    Task<IEnumerable<string>> FindAllWardsByDistrictNameQueryAsync(
        string districtName,
        CancellationToken cancellationToken
    );
}
