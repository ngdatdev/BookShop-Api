using BookShop.Data.Features.Repositories.Auth;
using BookShop.Data.Features.Repositories.Carts;
using BookShop.Data.Features.Repositories.Product;
using BookShop.Data.Features.Repositories.User;
using BookShop.Data.Features.UnitOfWork;
using BookShop.Data.Shared.Entities;
using BookShop.Data.Shared.Repositories.VerifyAccessToken;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.Auth;
using BookShop.PostgresSql.Repositories.Product;
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

    private IAuthFeatureRepository _authFeatureRepository;
    private IUserFeatureRepository _userFeatureRepository;
    private IProductFeatureRepository _productFeatureRepository;
    private ICartFeatureRepository _cartFeatureRepository;

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

    public IProductFeatureRepository ProductFeature
    {
        get
        {
            return _productFeatureRepository ??= new ProductFeatureRepository(context: _context);
        }
    }

    public ICartFeatureRepository CartFeature
    {
        get { return _cartFeatureRepository ??= new CartFeatureRepository(context: _context); }
    }
}
