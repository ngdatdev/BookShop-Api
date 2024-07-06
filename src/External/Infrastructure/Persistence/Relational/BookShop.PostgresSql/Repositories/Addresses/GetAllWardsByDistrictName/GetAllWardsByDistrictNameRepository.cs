using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.Addresses.GetAllWardsByDistrictName;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Addresses.GetAllWardsByDistrictName;

/// <summary>
///    Implement of IGetAllWardsByDistrictNameRepository repository.
/// </summary>
internal partial class GetAllWardsByDistrictNameRepository
    : IGetAllWardsByDistrictNameRepository
{
    private readonly BookShopContext _context;
    private DbSet<Address> _addresses;

    public GetAllWardsByDistrictNameRepository(BookShopContext context)
    {
        _context = context;
        _addresses = _context.Set<Address>();
    }
}
