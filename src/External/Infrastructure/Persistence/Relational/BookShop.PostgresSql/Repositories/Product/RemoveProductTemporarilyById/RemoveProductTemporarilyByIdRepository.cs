using BookShop.Data.Features.Repositories.Product.RemoveProductTemporarilyById;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Product.RemoveProductTemporarilyById;

/// <summary>
///    Implement of IRemoveProductTemporarilyById repository.
/// </summary>
internal partial class RemoveProductTemporarilyByIdRepository
    : IRemoveProductTemporarilyByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<BookShop.Data.Shared.Entities.Product> _products;

    public RemoveProductTemporarilyByIdRepository(BookShopContext context)
    {
        _context = context;
        _products = _context.Set<BookShop.Data.Shared.Entities.Product>();
    }
}
