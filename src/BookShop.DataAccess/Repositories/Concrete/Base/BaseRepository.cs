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
        await _dbSet.AddAsync(newEntity, cancellationToken: cancellationToken);
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
