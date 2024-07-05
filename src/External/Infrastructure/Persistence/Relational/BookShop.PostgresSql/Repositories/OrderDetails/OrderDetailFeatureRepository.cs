using BookShop.Data.Features.Repositories.OrderDetails;
using BookShop.Data.Features.Repositories.OrderDetails.GetOrderDetailById;
using BookShop.Data.Features.Repositories.OrderDetails.GetOrderDetailsByOrderStatusId;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.OrderDetails.GetOrderDetailById;
using BookShop.PostgresSql.Repositories.OrderDetails.GetOrderDetailsByOrderStatusId;

namespace BookShop.PostgresSql.Repositories.OrderDetails;

/// <summary>
///    Implement of CartFeatureRepository interface.
/// </summary>
internal class OrderDetailFeatureRepository : IOrderDetailFeatureRepository
{
    private readonly BookShopContext _context;
    private IGetOrderDetailByIdRepository _orderDetailByIdRepository;
    private IGetOrderDetailsByOrderStatusIdRepository _orderDetailsByOrderStatusIdRepository;

    internal OrderDetailFeatureRepository(BookShopContext context)
    {
        _context = context;
    }

    public IGetOrderDetailByIdRepository GetOrderDetailByIdRepository
    {
        get
        {
            return _orderDetailByIdRepository ??= new GetOrderDetailByIdRepository(
                context: _context
            );
        }
    }

    public IGetOrderDetailsByOrderStatusIdRepository GetOrderDetailsByOrderStatusIdRepository
    {
        get
        {
            return _orderDetailsByOrderStatusIdRepository ??=
                new GetOrderDetailsByOrderStatusIdRepository(context: _context);
        }
    }
}
