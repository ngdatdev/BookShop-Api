using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Data.Features.Repositories.User.UpdateUserById;

/// <summary>
///     Interface for Query UpdateUserById Repository.
/// </summary>
public partial interface IUpdateUserByIdRepository
{
    Task<Shared.Entities.User> FindUserByIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    );

    Task<bool> IsUserTemporarilyRemovedByIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    );

    Task<Guid> FindAddressIdFoundByNameQueryAsync(
        string ward,
        string district,
        string province,
        CancellationToken cancellationToken
    );
}
