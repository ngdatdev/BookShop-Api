using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.Orders.RemoveOrderTemporarilyById;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Orders.RemoveOrderTemporarilyById;

/// <summary>
///    Implement of IRemoveOrderTemporarilyById repository.
/// </summary>
internal partial class RemoveOrderTemporarilyByIdRepository : IRemoveOrderTemporarilyByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<BookShop.Data.Shared.Entities.Order> _orders;

    public RemoveOrderTemporarilyByIdRepository(BookShopContext context)
    {
        _context = context;
        _orders = _context.Set<BookShop.Data.Shared.Entities.Order>();
    }
}
