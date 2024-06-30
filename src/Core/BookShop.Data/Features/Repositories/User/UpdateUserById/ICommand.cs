using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.User.UpdateUserById;

/// <summary>
///     Interface for Command UpdateUserByIdRepository.
/// </summary>
public partial interface IUpdateUserByIdRepository
{
    Task<bool> UpdateUserByIdCommandAsync(
        Shared.Entities.User updateUser,
        Shared.Entities.User currentUser,
        CancellationToken cancellationToken
    );

    Task<bool> CreateAddressCommandAsync(
        Shared.Entities.Address address,
        CancellationToken cancellationToken
    );
}
