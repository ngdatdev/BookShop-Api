using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.DataAccess.Data;
using BookShop.DataAccess.Entities.Base;
using BookShop.DataAccess.Repositories.Interface.Base;
using Microsoft.EntityFrameworkCore;

namespace BookShop.DataAccess.Repositories.Concrete.Base;

/// <summary>
///     Implementation of base repository.
/// </summary>
/// <typeparam name="TEntity">
///     Represent the table of the database or
///     in the simple term, entity of the system.
/// </typeparam>
/// <remarks>
///     All repository classes must inherit from this
///     base class.
/// </remarks>
internal abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : class, IBaseEntity
{
    protected readonly DbSet<TEntity> _dbSet;

    private protected BaseRepository(BookShopContext context)
    {
        _dbSet = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task AddAsync(TEntity newEntity, CancellationToken cancellationToken)
    {
        // Validate new entity.
        if (Equals(objA: newEntity, objB: default))
        {
            throw new InvalidOperationException();
        }

        await _dbSet.AddAsync(entity: newEntity, cancellationToken: cancellationToken);
    }

    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
