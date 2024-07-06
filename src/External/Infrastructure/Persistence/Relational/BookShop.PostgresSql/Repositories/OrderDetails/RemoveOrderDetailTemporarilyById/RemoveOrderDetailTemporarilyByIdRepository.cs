using BookShop.Data.Features.Repositories.OrderDetails.RemoveOrderDetailTemporarilyById;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.OrderDetails.RemoveOrderDetailTemporarilyById;

/// <summary>
///    Implement of IRemoveOrderDetailTemporarilyByIdRepository repository.
/// </summary>
internal partial class RemoveOrderDetailTemporarilyByIdRepository
    : IRemoveOrderDetailTemporarilyByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<BookShop.Data.Shared.Entities.OrderDetail> _orderDetails;

    public RemoveOrderDetailTemporarilyByIdRepository(BookShopContext context)
    {
        _context = context;
        _orderDetails = _context.Set<BookShop.Data.Shared.Entities.OrderDetail>();
    }
}
