using BookShop.Data.Features.Repositories.Carts.GetCartById;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Carts.GetCartById;

/// <summary>
///    Implement of IGetCartById repository.
/// </summary>
internal partial class GetCartByIdRepository : IGetCartByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<Cart> _carts;

    public GetCartByIdRepository(BookShopContext context)
    {
        _context = context;
        _carts = _context.Set<Cart>();
    }
}
