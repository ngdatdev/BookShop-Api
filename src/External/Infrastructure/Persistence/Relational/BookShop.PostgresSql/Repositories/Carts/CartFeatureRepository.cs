using BookShop.Data.Features.Repositories.Carts;
using BookShop.Data.Features.Repositories.Carts.GetCartById;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.Carts.GetCartById;

namespace BookShop.PostgresSql.Repositories.Auth;

/// <summary>
///    Implement of CartFeatureRepository interface.
/// </summary>
internal class CartFeatureRepository : ICartFeatureRepository
{
    private readonly BookShopContext _context;
    private IGetCartByIdRepository _getCartByIdRepository;

    internal CartFeatureRepository(BookShopContext context)
    {
        _context = context;
    }

    public IGetCartByIdRepository GetCartByIdRepository
    {
        get { return _getCartByIdRepository ??= new GetCartByIdRepository(context: _context); }
    }
}
