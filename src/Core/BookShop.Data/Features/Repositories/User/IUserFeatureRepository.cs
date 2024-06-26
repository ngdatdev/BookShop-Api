using BookShop.Data.Features.Repositories.User.GetAllUsers;
using BookShop.Data.Features.Repositories.User.GetProfileUser;
using BookShop.Data.Features.Repositories.User.RemoveUserPermanentlyById;

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
}
