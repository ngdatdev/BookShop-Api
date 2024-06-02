using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Entities;
using BookShop.Data.Repositories.Interface.Base;

namespace BookShop.Data.Repositories.Interface;

/// <summary>
///     Represent methods that encapsulate queries
///     to interact with "Users" table.
/// </summary>
/// <remarks>
///     All repository interfaces must implement
///     <seealso cref="IBaseRepository{TEntity}"/> interface.
/// </remarks>
public interface IUserRepository : IBaseRepository<User>
{
    Task<User> FindUserByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> IsUserFoundByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<User> FindUserByUsernameAsync(string username, CancellationToken cancellationToken);
}
