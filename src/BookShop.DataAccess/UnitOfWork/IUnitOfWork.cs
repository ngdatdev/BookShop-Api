using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.DataAccess.Data;
using BookShop.DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookShop.DataAccess.UnitOfWork;

/// <summary>
///     Represent the base unit of work.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    ///     UserDetail comment repository.
    /// </summary>
    IUserDetailRepository UserDetailRepository { get; }

    /// <summary>
    ///     User comment repository.
    /// </summary>
    IUserRepository UserRepository { get; }

    /// <summary>
    ///     User comment repository.
    /// </summary>
    IRefreshTokenRepository RefreshTokenRepository { get; }

    /// <summary>
    ///     Begins a new transaction asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task CreateTransactionAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Creates a new execution strategy for handling transient failures.
    /// </summary>
    /// <returns>An instance of <see cref="IExecutionStrategy"/>.</returns>
    IExecutionStrategy CreateExecutionStrategy();

    /// <summary>
    ///     Commits the current transaction asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task CommitTransactionAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Rolls back the current transaction asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task RollBackTransactionAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Disposes of the current transaction asynchronously.
    /// </summary>
    /// <returns>A ValueTask that represents the asynchronous operation.</returns>
    ValueTask DisposeTransactionAsync();

    /// <summary>
    ///     Saves all changes made in this context to the database asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SaveToDatabaseAsync(CancellationToken cancellationToken);
}
