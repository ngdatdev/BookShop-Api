using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Addresses.GetAllTemporarilyAddresses;

/// <summary>
///     Interface for Query GetAllTemporarilyAddresses Repository
/// </summary>
public partial interface IGetAllTemporarilyAddressesRepository
{
    Task<IEnumerable<Shared.Entities.Address>> FindAllTemporarilyAddressesQueryAsync(
        CancellationToken cancellationToken
    );
}
