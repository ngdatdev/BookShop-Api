using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Addresses.UpdateAddressById;

/// <summary>
///     Interface for Command UpdateAddressById Repository
/// </summary>
public partial interface IUpdateAddressByIdRepository
{
    Task<bool> UpdateAddressByIdCommandAsync(
        Shared.Entities.Address currentAddress,
        Shared.Entities.Address updateAddress,
        CancellationToken cancellationToken
    );
}
