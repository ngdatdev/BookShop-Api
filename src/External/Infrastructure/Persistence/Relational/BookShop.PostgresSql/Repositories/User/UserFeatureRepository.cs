﻿using BookShop.Data.Features.Repositories.User;
using BookShop.Data.Features.Repositories.User.GetAllUsers;
using BookShop.Data.Features.Repositories.User.GetProfileUser;
using BookShop.Data.Features.Repositories.User.RemoveUserPermanentlyById;
using BookShop.Data.Features.Repositories.User.RemoveUserTemporarilyById;
using BookShop.Data.Features.Repositories.User.RestoreUserById;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.User.GetAllUsers;
using BookShop.PostgresSql.Repositories.User.GetProfileUser;
using BookShop.PostgresSql.Repositories.User.RemoveUserPermanentlyById;
using BookShop.PostgresSql.Repositories.User.RemoveUserTemporarilyById;
using BookShop.PostgresSql.Repositories.User.RestoreUserById;
using Microsoft.AspNetCore.Identity;

namespace BookShop.PostgresSql.Repositories.User;

/// <summary>
///    Implement of UserFeatureRepository interface.
/// </summary>
internal class UserFeatureRepository : IUserFeatureRepository
{
    private readonly BookShopContext _context;
    private readonly UserManager<BookShop.Data.Shared.Entities.User> _userManager;

    private IGetProfileUserRepository _getProfileUserRepository;
    private IGetAllUsersRepository _getAllUsersRepository;
    private IRemoveUserPermanentlyByIdRepository _removeUserPermanentlyByIdRepository;
    private IRemoveUserTemporarilyByIdRepository _removeUserTemporarilyByIdRepository;
    private IRestoreUserByIdRepository _restoreUserByIdRepository;

    internal UserFeatureRepository(
        BookShopContext context,
        UserManager<BookShop.Data.Shared.Entities.User> userManager
    )
    {
        _context = context;
        _userManager = userManager;
    }

    public IGetProfileUserRepository GetProfileUserRepository
    {
        get
        {
            return _getProfileUserRepository ??= new GetProfileUserRepository(context: _context);
        }
    }

    public IGetAllUsersRepository GetAllUsersRepository
    {
        get { return _getAllUsersRepository ??= new GetAllUsersRepository(context: _context); }
    }

    public IRemoveUserPermanentlyByIdRepository RemoveUserPermanentlyByIdRepository
    {
        get
        {
            return _removeUserPermanentlyByIdRepository ??= new RemoveUserPermanentlyByIdRepository(
                context: _context
            );
        }
    }

    public IRemoveUserTemporarilyByIdRepository RemoveUserTemporarilyByIdRepository
    {
        get
        {
            return _removeUserTemporarilyByIdRepository ??= new RemoveUserTemporarilyByIdRepository(
                context: _context
            );
        }
    }

    public IRestoreUserByIdRepository RestoreUserByIdRepository
    {
        get
        {
            return _restoreUserByIdRepository ??= new RestoreUserByIdRepository(context: _context);
        }
    }
}
