using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.Carts.ClearCart;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Carts.ClearCart;

/// <summary>
///    Implement of IClearCart repository.
/// </summary>
internal partial class ClearCartRepository : IClearCartRepository
{
    private readonly BookShopContext _context;
    private DbSet<Cart> _carts;
    private DbSet<CartItem> _cartItems;

    public ClearCartRepository(BookShopContext context)
    {
        _context = context;
        _carts = _context.Set<Cart>();
        _cartItems = _context.Set<CartItem>();
    }
}
