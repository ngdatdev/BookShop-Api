using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.Addresses.RestoreAddressById;

/// <summary>
///     Interface for Command RestoreAddressById Repository
/// </summary>
public partial interface IRestoreAddressByIdRepository
{
    Task<bool> RestoreAddressByIdCommandAsync(Guid addressId, CancellationToken cancellationToken);
}
