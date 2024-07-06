using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.Addresses.GetAddressesByWard;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Addresses.GetAddressesByWard;

/// <summary>
///    Implement of IGetAddressesByWardRepository repository.
/// </summary>
internal partial class GetAddressesByWardRepository : IGetAddressesByWardRepository
{
    private readonly BookShopContext _context;
    private DbSet<Address> _addresses;

    public GetAddressesByWardRepository(BookShopContext context)
    {
        _context = context;
        _addresses = _context.Set<Address>();
    }
}
