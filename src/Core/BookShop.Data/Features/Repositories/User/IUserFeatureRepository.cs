﻿using BookShop.Data.Features.Repositories.User.GetAllUsers;
using BookShop.Data.Features.Repositories.User.GetAllUsersTemporarilyRemovedById;
using BookShop.Data.Features.Repositories.User.GetProfileUser;
using BookShop.Data.Features.Repositories.User.RemoveUserPermanentlyById;
using BookShop.Data.Features.Repositories.User.RemoveUserTemporarilyById;
using BookShop.Data.Features.Repositories.User.RestoreUserById;
using BookShop.Data.Features.Repositories.User.UpdateUserById;

namespace BookShop.Data.Features.Repositories.User;

/// <summary>
///     Interface for user repository manager.
/// </summary>
public interface IUserFeatureRepository
{
    /// <summary>
    ///     Gets get profile user repository.
    /// </summary>
    public IGetProfileUserRepository GetProfileUserRepository { get; }

    /// <summary>
    ///     Gets get all users repository.
    /// </summary>
    public IGetAllUsersRepository GetAllUsersRepository { get; }

    /// <summary>
    ///     Gets remove user permanently by id repository.
    /// </summary>
    public IRemoveUserPermanentlyByIdRepository RemoveUserPermanentlyByIdRepository { get; }

    /// <summary>
    ///     Gets remove user temporarily by id repository.
    /// </summary>
    public IRemoveUserTemporarilyByIdRepository RemoveUserTemporarilyByIdRepository { get; }

    /// <summary>
    ///     Gets restore user by id repository.
    /// </summary>
    public IRestoreUserByIdRepository RestoreUserByIdRepository { get; }

    /// <summary>
    ///     Gets update user by id repository.
    /// </summary>
    public IUpdateUserByIdRepository UpdateUserByIdRepository { get; }

    /// <summary>
    ///     Gets get all users temporarily removed by id repository.
    /// </summary>
    public IGetAllUsersTemporarilyRemovedByIdRepository GetAllUsersTemporarilyRemovedByIdRepository { get; }
}
