using BookShop.Data.Features.Repositories.Auth;
using BookShop.Data.Features.Repositories.User;
using BookShop.Data.Features.UnitOfWork;
using BookShop.Data.Shared.Entities;
using BookShop.Data.Shared.Repositories.VerifyAccessToken;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.Auth;
using BookShop.PostgresSql.Repositories.User;
using Microsoft.AspNetCore.Identity;

namespace BookShop.PostgresSql.UnitOfWorks;

/// <summary>
///     Implementation of unit of work interface.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly BookShopContext _context;
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;

    private IVerifyAccessTokenRepository _verifyAccessTokenRepository;
    private IAuthFeatureRepository _authFeatureRepository;
    private IUserFeatureRepository _userFeatureRepository;

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

    public IAuthFeatureRepository AuthFeature
    {
        get { return _authFeatureRepository ??= new AuthFeatureRepository(context: _context); }
    }

    public IUserFeatureRepository UserFeature
    {
        get
        {
            return _userFeatureRepository ??= new UserFeatureRepository(
                context: _context,
                userManager: _userManager
            );
        }
    }
}