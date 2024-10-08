using BookShop.Data.Features.Repositories.Orders;
using BookShop.Data.Features.Repositories.Orders.CreateOrder;
using BookShop.Data.Features.Repositories.Orders.GetAllOrders;
using BookShop.Data.Features.Repositories.Orders.GetAllTemporarilyRemovedOrder;
using BookShop.Data.Features.Repositories.Orders.GetOrderById;
using BookShop.Data.Features.Repositories.Orders.GetOrdersByUserId;
using BookShop.Data.Features.Repositories.Orders.RemoveOrderPermanentlyById;
using BookShop.Data.Features.Repositories.Orders.RemoveOrderTemporarilyById;
using BookShop.Data.Features.Repositories.Orders.RestoreOrderById;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.Orders.CreateOrder;
using BookShop.PostgresSql.Repositories.Orders.GetAllOrders;
using BookShop.PostgresSql.Repositories.Orders.GetAllTemporarilyRemovedOrder;
using BookShop.PostgresSql.Repositories.Orders.GetOrderById;
using BookShop.PostgresSql.Repositories.Orders.GetOrdersByUserId;
using BookShop.PostgresSql.Repositories.Orders.RemoveOrderPermanentlyById;
using BookShop.PostgresSql.Repositories.Orders.RemoveOrderTemporarilyById;
using BookShop.PostgresSql.Repositories.Orders.RestoreOrderById;

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
    private IRemoveOrderTemporarilyByIdRepository _removeOrderTemporarilyByIdRepository;
    private IRestoreOrderByIdRepository _restoreOrderByIdRepository;
    private IGetAllTemporarilyRemovedOrderRepository _getAllTemporarilyRemovedOrderRepository;
    private IRemoveOrderPermanentlyByIdRepository _removeOrderPermanentlyByIdRepository;

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

    public IRemoveOrderTemporarilyByIdRepository RemoveOrderTemporarilyByIdRepository
    {
        get
        {
            return _removeOrderTemporarilyByIdRepository ??=
                new RemoveOrderTemporarilyByIdRepository(context: _context);
        }
    }

    public IRestoreOrderByIdRepository RestoreOrderByIdRepository
    {
        get
        {
            return _restoreOrderByIdRepository ??= new RestoreOrderByIdRepository(
                context: _context
            );
        }
    }

    public IGetAllTemporarilyRemovedOrderRepository GetAllTemporarilyRemovedOrderRepository
    {
        get
        {
            return _getAllTemporarilyRemovedOrderRepository ??=
                new GetAllTemporarilyRemovedOrderRepository(context: _context);
        }
    }

    public IRemoveOrderPermanentlyByIdRepository RemoveOrderPermanentlyByIdRepository
    {
        get
        {
            return _removeOrderPermanentlyByIdRepository ??=
                new RemoveOrderPermanentlyByIdRepository(context: _context);
        }
    }
}
