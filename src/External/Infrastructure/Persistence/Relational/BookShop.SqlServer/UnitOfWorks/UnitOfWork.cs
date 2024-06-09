using BookShop.Data.Features.Repositories.Login;
using BookShop.Data.Features.UnitOfWork;
using BookShop.Data.Shared.Entities;
using BookShop.Data.Shared.Repositories.VerifyAccessToken;
using BookShop.SqlServer.Data;
using BookShop.SqlServer.Repositories.Login;
using BookShop.SqlServer.Repositories.VerifyDataAccess;
using Microsoft.AspNetCore.Identity;

namespace BookShop.SqlServer.UnitOfWorks;

/// <summary>
///     Implementation of unit of work interface.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly BookShopContext _context;
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;

    private IVerifyAccessTokenRepository _verifyAccessTokenRepository;
    private ILoginRepository _loginRepository;

    public UnitOfWork(
        BookShopContext context,
        RoleManager<Role> roleManager,
        UserManager<User> userManager
    )
    {
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public IVerifyAccessTokenRepository VerifyAccessTokenRepository
    {
        get
        {
            return _verifyAccessTokenRepository ??= new VerifyDataAccessRepository(
                context: _context
            );
        }
    }

    public ILoginRepository LoginRepository
    {
        get { return _loginRepository ??= new LoginRepository(context: _context); }
    }
}
