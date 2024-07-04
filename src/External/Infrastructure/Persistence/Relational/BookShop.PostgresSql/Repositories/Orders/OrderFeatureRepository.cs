using BookShop.Data.Features.Repositories.Orders;
using BookShop.Data.Features.Repositories.Orders.CreateOrder;
using BookShop.Data.Features.Repositories.Orders.GetAllOrders;
using BookShop.Data.Features.Repositories.Orders.GetOrderById;
using BookShop.Data.Features.Repositories.Orders.GetOrdersByUserId;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.Orders.CreateOrder;
using BookShop.PostgresSql.Repositories.Orders.GetAllOrders;
using BookShop.PostgresSql.Repositories.Orders.GetOrderById;
using BookShop.PostgresSql.Repositories.Orders.GetOrdersByUserId;

namespace BookShop.PostgresSql.Repositories.Orders;

/// <summary>
///    Implement of OrderFeatureRepository interface.
/// </summary>
internal class OrderFeatureRepository : IOrderFeatureRepository
{
    private readonly BookShopContext _context;
    private ICreateOrderRepository _createOrderRepository;
    private IGetOrderByIdRepository _getOrderByIdRepository;
    private IGetOrdersByUserIdRepository _getOrdersByUserIdRepository;
    private IGetAllOrdersRepository _getAllOrdersRepository;

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

    public IGetOrdersByUserIdRepository GetOrdersByUserIdRepository
    {
        get
        {
            return _getOrdersByUserIdRepository ??= new GetOrdersByUserIdRepository(
                context: _context
            );
        }
    }

    public IGetAllOrdersRepository GetAllOrdersRepository
    {
        get { return _getAllOrdersRepository ??= new GetAllOrdersRepository(context: _context); }
    }
}
