using BookShop.Data.Features.Repositories.OrderDetails;
using BookShop.Data.Features.Repositories.OrderDetails.GetAllOrderDetailsByUserId;
using BookShop.Data.Features.Repositories.OrderDetails.GetOrderDetailById;
using BookShop.Data.Features.Repositories.OrderDetails.GetOrderDetailsByOrderStatusId;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.OrderDetails.GetAllOrderDetailsByUserId;
using BookShop.PostgresSql.Repositories.OrderDetails.GetOrderDetailById;
using BookShop.PostgresSql.Repositories.OrderDetails.GetOrderDetailsByOrderStatusId;

namespace BookShop.PostgresSql.Repositories.OrderDetails;

/// <summary>
///    Implement of CartFeatureRepository interface.
/// </summary>
internal class OrderDetailFeatureRepository : IOrderDetailFeatureRepository
{
    private readonly BookShopContext _context;
    private IGetOrderDetailByIdRepository _getOrderDetailByIdRepository;
    private IGetOrderDetailsByOrderStatusIdRepository _getOrderDetailsByOrderStatusIdRepository;
    private IGetAllOrderDetailsByUserIdRepository _getAllOrderDetailsByUserIdRepository;

    internal OrderDetailFeatureRepository(BookShopContext context)
    {
        _context = context;
    }

    public IGetOrderDetailByIdRepository GetOrderDetailByIdRepository
    {
        get
        {
            return _getOrderDetailByIdRepository ??= new GetOrderDetailByIdRepository(
                context: _context
            );
        }
    }

    public IGetOrderDetailsByOrderStatusIdRepository GetOrderDetailsByOrderStatusIdRepository
    {
        get
        {
            return _getOrderDetailsByOrderStatusIdRepository ??=
                new GetOrderDetailsByOrderStatusIdRepository(context: _context);
        }
    }

    public IGetAllOrderDetailsByUserIdRepository GetAllOrderDetailsByUserIdRepository
    {
        get
        {
            return _getAllOrderDetailsByUserIdRepository ??=
                new GetAllOrderDetailsByUserIdRepository(context: _context);
        }
    }
}
