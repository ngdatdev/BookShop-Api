using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.DataAccess.Data;
using BookShop.DataAccess.Repositories.Concrete;
using BookShop.DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookShop.DataAccess.UnitOfWork;

/// <summary>
///     Implementation of unit of work interface.
/// </summary>
public class SqlUnitOfWork : IUnitOfWork
{
    private IDbContextTransaction _dbTransaction;
    private readonly BookShopContext _context;
    private IUserDetailRepository _userDetailRepository;
    private IRefreshTokenRepository _refreshTokenRepository;

    public IUserDetailRepository UserDetailRepository
    {
        get
        {
            _userDetailRepository ??= new UserDetailRepository(context: _context);

            return _userDetailRepository;
        }
    }

    public IRefreshTokenRepository RefreshTokenRepository
    {
        get
        {
            _refreshTokenRepository ??= new RefreshTokenRepository(context: _context);
            return _refreshTokenRepository;
        }
    }

    public IUserRepository UserRepository => throw new NotImplementedException();

    public SqlUnitOfWork(BookShopContext context)
    {
        _context = context;
    }

    public async Task CreateTransactionAsync(CancellationToken cancellationToken)
    {
        _dbTransaction = await _context.Database.BeginTransactionAsync(
            cancellationToken: cancellationToken
        );
    }

    public IExecutionStrategy CreateExecutionStrategy()
    {
        return _context.Database.CreateExecutionStrategy();
    }

    public Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        return _dbTransaction.CommitAsync(cancellationToken: cancellationToken);
    }

    public Task RollBackTransactionAsync(CancellationToken cancellationToken)
    {
        return _dbTransaction.RollbackAsync(cancellationToken: cancellationToken);
    }

    public ValueTask DisposeTransactionAsync()
    {
        return _dbTransaction.DisposeAsync();
    }

    public Task SaveToDatabaseAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken: cancellationToken);
    }
}
