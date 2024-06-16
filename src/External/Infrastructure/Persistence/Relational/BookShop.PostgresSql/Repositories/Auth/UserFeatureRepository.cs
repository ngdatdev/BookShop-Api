using BookShop.Data.Features.Repositories.Auth;
using BookShop.Data.Features.Repositories.Auth.ChangingPassword;
using BookShop.Data.Features.Repositories.Auth.ForgotPassword;
using BookShop.Data.Features.Repositories.Auth.Login;
using BookShop.Data.Features.Repositories.Auth.Logout;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.Auth.ChangingPassword;
using BookShop.PostgresSql.Repositories.Auth.ForgotPassword;
using BookShop.PostgresSql.Repositories.Auth.Login;
using BookShop.PostgresSql.Repositories.Auth.Logout;

namespace BookShop.PostgresSql.Repositories.Auth;

/// <summary>
///    Implement of AuthFeatureRepository interface.
/// </summary>
internal class AuthFeatureRepository : IAuthFeatureRepository
{
    private readonly BookShopContext _context;
    private ILoginRepository _loginRepository;
    private ILogoutRepository _logoutRepository;
    private IForgotPasswordRepository _forgotPasswordRepository;
    private IChangingPasswordRepository _changingPasswordRepository;

    internal AuthFeatureRepository(BookShopContext context)
    {
        _context = context;
    }

    public ILoginRepository LoginRepository
    {
        get { return _loginRepository ??= new LoginRepository(context: _context); }
    }

    public ILogoutRepository LogoutRepository
    {
        get { return _logoutRepository ??= new LogoutRepository(context: _context); }
    }

    public IForgotPasswordRepository ForgotPasswordRepository
    {
        get
        {
            return _forgotPasswordRepository ??= new ForgotPasswordRepository(context: _context);
        }
    }

    public IChangingPasswordRepository ChangingPasswordRepository
    {
        get
        {
            return _changingPasswordRepository ??= new ChangingPasswordRepository(
                context: _context
            );
        }
    }
}
