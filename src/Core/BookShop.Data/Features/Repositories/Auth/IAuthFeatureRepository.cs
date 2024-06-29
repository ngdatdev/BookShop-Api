using BookShop.Data.Features.Repositories.Auth.ChangingPassword;
using BookShop.Data.Features.Repositories.Auth.ConfirmUserRegistrationEmail;
using BookShop.Data.Features.Repositories.Auth.ForgotPassword;
using BookShop.Data.Features.Repositories.Auth.Login;
using BookShop.Data.Features.Repositories.Auth.Logout;
using BookShop.Data.Features.Repositories.Auth.RefreshAccessToken;
using BookShop.Data.Features.Repositories.Auth.RegisterAsUser;

namespace BookShop.Data.Features.Repositories.Auth;

/// <summary>
///     Interface for auth repository manager.
/// </summary>
public interface IAuthFeatureRepository
{
    /// <summary>
    ///     Gets login feature repository.
    /// </summary>
    public ILoginRepository LoginRepository { get; }

    /// <summary>
    ///     Gets forgot password feature repository.
    /// </summary>
    public IForgotPasswordRepository ForgotPasswordRepository { get; }

    /// <summary>
    ///     Gets logout feature repository.
    /// </summary>
    public ILogoutRepository LogoutRepository { get; }

    /// <summary>
    ///     Gets changing password feature repository.
    /// </summary>
    public IChangingPasswordRepository ChangingPasswordRepository { get; }

    /// <summary>
    ///     Gets refresh access token feature repository.
    /// </summary>
    public IRefreshAccessTokenRepository RefreshAccessTokenRepository { get; }

    /// <summary>
    ///     Gets register as user feature repository.
    /// </summary>
    public IRegisterAsUserRepository RegisterAsUserRepository { get; }

    /// <summary>
    ///     Gets confirm user registration email feature repository.
    /// </summary>
    public IConfirmUserRegistrationEmailRepository ConfirmUserRegistrationEmailRepository { get; }
}
