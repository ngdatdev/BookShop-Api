using BookShop.Data.Features.Repositories.OrderDetails;
using BookShop.Data.Features.Repositories.OrderDetails.GetOrderDetailById;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.OrderDetails.GetOrderDetailById;

namespace BookShop.PostgresSql.Repositories.OrderDetails;

/// <summary>
///    Implement of CartFeatureRepository interface.
/// </summary>
internal class OrderDetailFeatureRepository : IOrderDetailFeatureRepository
{
    private readonly BookShopContext _context;
    private IGetOrderDetailByIdRepository _orderDetailByIdRepository;

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
}
