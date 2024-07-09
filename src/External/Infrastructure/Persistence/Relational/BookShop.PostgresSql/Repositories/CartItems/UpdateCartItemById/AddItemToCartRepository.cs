using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.CartItems.UpdateCartItemById;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.CartItems.UpdateCartItemById;

/// <summary>
///    Implement of IUpdateCartItemById repository.
/// </summary>
internal partial class UpdateCartItemByIdRepository : IUpdateCartItemByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<CartItem> _cartItems;

    public UpdateCartItemByIdRepository(BookShopContext context)
    {
        _context = context;
        _cartItems = _context.Set<CartItem>();
    }
}
