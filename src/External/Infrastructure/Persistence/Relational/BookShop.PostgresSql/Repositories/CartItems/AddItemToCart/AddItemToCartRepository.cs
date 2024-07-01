using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.CartItems.AddItemToCart;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.CartItems.AddItemToCart;

/// <summary>
///    Implement of IAddItemToCartRepository repository.
/// </summary>
internal partial class AddItemToCartRepository : IAddItemToCartRepository
{
    private readonly BookShopContext _context;
    private DbSet<CartItem> _cartItems;

    public AddItemToCartRepository(BookShopContext context)
    {
        _context = context;
        _cartItems = _context.Set<CartItem>();
    }
}
