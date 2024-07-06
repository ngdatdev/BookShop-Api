using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.Addresses.GetAllDistrictsByProvinceName;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Addresses.GetAllDistrictsByProvinceName;

/// <summary>
///    Implement of IGetAllDistrictsByProvinceNameRepository repository.
/// </summary>
internal partial class GetAllDistrictsByProvinceNameRepository
    : IGetAllDistrictsByProvinceNameRepository
{
    private readonly BookShopContext _context;
    private DbSet<Address> _addresses;

    public GetAllDistrictsByProvinceNameRepository(BookShopContext context)
    {
        _context = context;
        _addresses = _context.Set<Address>();
    }
}
