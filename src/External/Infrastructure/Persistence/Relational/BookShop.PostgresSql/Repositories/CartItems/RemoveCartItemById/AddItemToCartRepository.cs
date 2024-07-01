using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.CartItems.RemoveCartItemById;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.CartItems.RemoveCartItemById;

/// <summary>
///    Implement of IRemoveCartItemByIdRepository repository.
/// </summary>
internal partial class RemoveCartItemByIdRepository : IRemoveCartItemByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<CartItem> _cartItems;

    public RemoveCartItemByIdRepository(BookShopContext context)
    {
        _context = context;
        _cartItems = _context.Set<CartItem>();
    }
}
