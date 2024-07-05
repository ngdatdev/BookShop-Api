using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Shared.Entities;

namespace BookShop.Data.Features.Repositories.Roles.GetAllRolesTemporarilyRemoved;

/// <summary>
///     Interface for Query GetAllRolesTemporarilyRemovedRepository Repository
/// </summary>
public partial interface IGetAllRolesTemporarilyRemovedRepository
{
    Task<IEnumerable<Role>> FindAllRolesTemporarilyRemovedQueryAsync(
        CancellationToken cancellationToken
    );
}
