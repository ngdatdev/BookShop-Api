using BookShop.Data.Features.Repositories.CartItems.AddItemToCart;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.CartItems.AddItemToCart;

/// <summary>
///    Implement of IAddItemToCart repository.
/// </summary>
internal partial class AddItemToCartRepository : IAddItemToCartRepository
{
    private readonly BookShopContext _context;
    private DbSet<CartItem> _cartItems;
    private DbSet<Cart> _carts;
    private DbSet<BookShop.Data.Shared.Entities.Product> _products;

    public AddItemToCartRepository(BookShopContext context)
    {
        _context = context;
        _cartItems = _context.Set<CartItem>();
        _carts = _context.Set<Cart>();
        _products = _context.Set<BookShop.Data.Shared.Entities.Product>();
    }
}
