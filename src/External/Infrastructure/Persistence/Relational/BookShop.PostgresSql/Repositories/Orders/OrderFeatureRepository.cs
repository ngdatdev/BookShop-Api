using BookShop.Data.Features.Repositories.Orders;
using BookShop.Data.Features.Repositories.Orders.CreateOrder;
using BookShop.Data.Features.Repositories.Orders.GetOrderById;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.Orders.CreateOrder;
using BookShop.PostgresSql.Repositories.Orders.GetOrderById;

namespace BookShop.PostgresSql.Repositories.Orders;

/// <summary>
///    Implement of OrderFeatureRepository interface.
/// </summary>
internal class OrderFeatureRepository : IOrderFeatureRepository
{
    private readonly BookShopContext _context;
    ICreateOrderRepository _createOrderRepository;
    IGetOrderByIdRepository _getOrderByIdRepository;

    internal OrderFeatureRepository(BookShopContext context)
    {
        _context = context;
    }

    public ICreateOrderRepository CreateOrderRepository
    {
        get { return _createOrderRepository ??= new CreateOrderRepository(context: _context); }
    }

    public IGetOrderByIdRepository GetOrderByIdRepository
    {
        get { return _getOrderByIdRepository ??= new GetOrderByIdRepository(context: _context); }
    }
}
