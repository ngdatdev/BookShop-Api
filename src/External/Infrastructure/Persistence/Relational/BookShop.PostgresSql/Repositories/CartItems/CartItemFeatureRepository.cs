using BookShop.Data.Features.Repositories.CartItems;
using BookShop.Data.Features.Repositories.CartItems.AddItemToCart;
using BookShop.Data.Features.Repositories.CartItems.UpdateCartItemById;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.CartItems.AddItemToCart;
using BookShop.PostgresSql.Repositories.CartItems.UpdateCartItemById;

namespace BookShop.PostgresSql.Repositories.CartItems;

/// <summary>
///    Implement of CartItemFeatureRepository interface.
/// </summary>
internal class CartItemFeatureRepository : ICartItemFeatureRepository
{
    private readonly BookShopContext _context;
    private IAddItemToCartRepository _addItemToCartRepository;
    private IUpdateCartItemByIdRepository _updateCartItemByIdRepository;

    internal CartItemFeatureRepository(BookShopContext context)
    {
        _context = context;
    }

    public IAddItemToCartRepository AddItemToCartRepository
    {
        get { return _addItemToCartRepository ??= new AddItemToCartRepository(context: _context); }
    }

    public IUpdateCartItemByIdRepository UpdateCartItemByIdRepository
    {
        get
        {
            return _updateCartItemByIdRepository ??= new UpdateCartItemByIdRepository(
                context: _context
            );
        }
    }
}
