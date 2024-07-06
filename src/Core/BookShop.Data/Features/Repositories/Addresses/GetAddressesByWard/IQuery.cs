using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Addresses.GetAddressesByWard;

/// <summary>
///     Interface for Query GetAddressesByWardRepository Repository
/// </summary>
public partial interface IGetAddressesByWardRepository
{
    Task<IEnumerable<Shared.Entities.Address>> FindAllAddressesByWardNameQueryAsync(
        string ward,
        CancellationToken cancellationToken
    );
}
