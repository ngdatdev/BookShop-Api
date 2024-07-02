using BookShop.Data.Features.Repositories.Orders;
using BookShop.Data.Features.Repositories.Orders.CreateOrder;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.Orders.CreateOrder;

namespace BookShop.PostgresSql.Repositories.Orders;

/// <summary>
///    Implement of OrderFeatureRepository interface.
/// </summary>
internal class OrderFeatureRepository : IOrderFeatureRepository
{
    private readonly BookShopContext _context;
    ICreateOrderRepository _createOrderRepository;

    internal OrderFeatureRepository(BookShopContext context)
    {
        _context = context;
    }

    public ICreateOrderRepository CreateOrderRepository
    {
        get { return _createOrderRepository ??= new CreateOrderRepository(context: _context); }
    }
}
