using BookShop.Data.Features.Repositories.Address;
using BookShop.Data.Features.Repositories.Auth;
using BookShop.Data.Features.Repositories.CartItems;
using BookShop.Data.Features.Repositories.Carts;
using BookShop.Data.Features.Repositories.OrderDetails;
using BookShop.Data.Features.Repositories.Orders;
using BookShop.Data.Features.Repositories.Payments;
using BookShop.Data.Features.Repositories.Product;
using BookShop.Data.Features.Repositories.Reviews;
using BookShop.Data.Features.Repositories.Roles;
using BookShop.Data.Features.Repositories.User;
using BookShop.Data.Features.UnitOfWork;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.Auth;
using BookShop.PostgresSql.Repositories.CartItems;
using BookShop.PostgresSql.Repositories.Carts;
using BookShop.PostgresSql.Repositories.OrderDetails;
using BookShop.PostgresSql.Repositories.Orders;
using BookShop.PostgresSql.Repositories.Payments;
using BookShop.PostgresSql.Repositories.Product;
using BookShop.PostgresSql.Repositories.Reviews;
using BookShop.PostgresSql.Repositories.Roles;
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
    private ICartItemFeatureRepository _cartItemFeatureRepository;
    private IOrderFeatureRepository _orderFeatureRepository;
    private IRoleFeatureRepository _roleFeatureRepository;
    private IOrderDetailFeatureRepository _orderDetailFeatureRepository;
    private IAddressFeatureRepository _addressFeatureRepository;
    private IReviewFeatureRepository _reviewFeatureRepository;
    private IPaymentFeatureRepository _paymentFeatureRepository;

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

    public ICartItemFeatureRepository CartItemFeature
    {
        get
        {
            return _cartItemFeatureRepository ??= new CartItemFeatureRepository(context: _context);
        }
    }

    public IOrderFeatureRepository OrderFeature
    {
        get { return _orderFeatureRepository ??= new OrderFeatureRepository(context: _context); }
    }

    public IRoleFeatureRepository RoleFeature
    {
        get { return _roleFeatureRepository ??= new RoleFeatureRepository(context: _context); }
    }

    public IOrderDetailFeatureRepository OrderDetailFeature
    {
        get
        {
            return _orderDetailFeatureRepository ??= new OrderDetailFeatureRepository(
                context: _context
            );
        }
    }

    public IAddressFeatureRepository AddressFeature
    {
        get
        {
            return _addressFeatureRepository ??= new AddressFeatureRepository(context: _context);
        }
    }

    public IReviewFeatureRepository ReviewFeature
    {
        get { return _reviewFeatureRepository ??= new ReviewFeatureRepository(context: _context); }
    }

    public IPaymentFeatureRepository PaymentFeature
    {
        get
        {
            return _paymentFeatureRepository ??= new PaymentFeatureRepository(context: _context);
        }
    }
}
