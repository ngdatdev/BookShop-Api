using BookShop.Data.Features.Repositories.Orders.CreateOrder;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Orders.CreateOrder;

/// <summary>
///    Implement of ICreateOrder repository.
/// </summary>
internal partial class CreateOrderRepository : ICreateOrderRepository
{
    private readonly BookShopContext _context;
    private DbSet<Order> _orders;
    private DbSet<OrderDetail> _orderDetails;
    private DbSet<CartItem> _cartItems;
    private DbSet<Address> _addresses;
    private DbSet<BookShop.Data.Shared.Entities.Product> _products;

    public CreateOrderRepository(BookShopContext context)
    {
        _context = context;
        _orders = _context.Set<Order>();
        _orderDetails = _context.Set<OrderDetail>();
        _cartItems = _context.Set<CartItem>();
        _addresses = _context.Set<Address>();
        _products = _context.Set<BookShop.Data.Shared.Entities.Product>();
    }
}
