using BookShop.Data.Features.Repositories.OrderDetails.RestoreOrderDetailById;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.OrderDetails.RestoreOrderDetailById;

/// <summary>
///    Implement of IRestoreOrderDetailByIdRepository repository.
/// </summary>
internal partial class RestoreOrderDetailByIdRepository : IRestoreOrderDetailByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<BookShop.Data.Shared.Entities.OrderDetail> _orderDetails;

    public RestoreOrderDetailByIdRepository(BookShopContext context)
    {
        _context = context;
        _orderDetails = _context.Set<BookShop.Data.Shared.Entities.OrderDetail>();
    }
}
