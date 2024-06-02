using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Entities;
using BookShop.Data.Repositories.Interface.Base;
using BookShop.DataAccess.Entities.Base;

namespace BookShop.Data.Repositories.Interface;

/// <summary>
///     Represent methods that encapsulate queries
///     to interact with "UserDetails" table.
/// </summary>
/// <remarks>
///     All repository interfaces must implement
///     <seealso cref="IBaseRepository{TEntity}"/> interface.
/// </remarks>
public interface IRoleDetailRepository : IBaseRepository<RoleDetail> { }
