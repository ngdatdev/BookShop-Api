using BookShop.Data.Features.Repositories.Carts;
using BookShop.Data.Features.Repositories.Carts.ClearCart;
using BookShop.Data.Features.Repositories.Carts.GetCartById;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.Carts.ClearCart;
using BookShop.PostgresSql.Repositories.Carts.GetCartById;

namespace BookShop.PostgresSql.Repositories.Auth;

/// <summary>
///    Implement of CartFeatureRepository interface.
/// </summary>
internal class CartFeatureRepository : ICartFeatureRepository
{
    private readonly BookShopContext _context;
    private IGetCartByIdRepository _getCartByIdRepository;
    private IClearCartRepository _clearCartRepository;

    internal CartFeatureRepository(BookShopContext context)
    {
        _context = context;
    }

    public IGetCartByIdRepository GetCartByIdRepository
    {
        get { return _getCartByIdRepository ??= new GetCartByIdRepository(context: _context); }
    }

    public IClearCartRepository ClearCartRepository
    {
        get { return _clearCartRepository ??= new ClearCartRepository(context: _context); }
    }
}
