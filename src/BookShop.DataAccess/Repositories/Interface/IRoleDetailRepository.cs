using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.DataAccess.Entities;
using BookShop.DataAccess.Entities.Base;
using BookShop.DataAccess.Repositories.Interface.Base;

namespace BookShop.DataAccess.Repositories.Interface;

/// <summary>
///     Represent methods that encapsulate queries
///     to interact with "UserDetails" table.
/// </summary>
/// <remarks>
///     All repository interfaces must implement
///     <seealso cref="IBaseRepository{TEntity}"/> interface.
/// </remarks>
public interface IRoleDetailRepository : IBaseRepository<RoleDetail> { }
