using BookShop.Data.Features.Repositories.Login;
using BookShop.Data.Shared.Repositories.VerifyAccessToken;

namespace BookShop.Data.Features.UnitOfWork;

/// <summary>
///     Represent the base unit of work.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    ///     Verify access token repository.
    /// </summary>
    public IVerifyAccessTokenRepository VerifyAccessTokenRepository { get; }

    /// <summary>
    ///    Login repository.
    /// </summary>
    public ILoginRepository LoginRepository { get; }
}
