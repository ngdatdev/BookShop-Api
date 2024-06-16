using BookShop.Data.Features.Repositories.Auth;
using BookShop.Data.Features.Repositories.User;
using BookShop.Data.Shared.Repositories.VerifyAccessToken;

namespace BookShop.Data.Features.UnitOfWork;

/// <summary>
///     Represent the base unit of work.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    ///    Auth repository manager.
    /// </summary>
    public IAuthFeatureRepository AuthFeature { get; }

    /// <summary>
    ///    User repository manager.
    /// </summary>
    public IUserFeatureRepository UserFeature { get; }
}
