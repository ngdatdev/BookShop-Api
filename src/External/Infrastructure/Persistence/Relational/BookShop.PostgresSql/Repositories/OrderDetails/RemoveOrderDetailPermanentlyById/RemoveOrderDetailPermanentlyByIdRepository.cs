using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.OrderDetails.RemoveOrderDetailPermanentlyById;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.OrderDetails.RemoveOrderDetailPermanentlyById;

/// <summary>
///    Implement of IRemoveOrderDetailPermanentlyById repository.
/// </summary>
internal partial class RemoveOrderDetailPermanentlyByIdRepository
    : IRemoveOrderDetailPermanentlyByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<OrderDetail> _orderDetails;

    public RemoveOrderDetailPermanentlyByIdRepository(BookShopContext context)
    {
        _context = context;
        _orderDetails = _context.Set<OrderDetail>();
    }
}
