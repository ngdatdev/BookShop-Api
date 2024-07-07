using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.Addresses.GetAllTemporarilyAddresses;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Addresses.GetAllTemporarilyAddresses;

/// <summary>
///    Implement of IGetAllTemporarilyAddressesRepository repository.
/// </summary>
internal partial class GetAllTemporarilyAddressesRepository : IGetAllTemporarilyAddressesRepository
{
    private readonly BookShopContext _context;
    private DbSet<Address> _addresses;

    public GetAllTemporarilyAddressesRepository(BookShopContext context)
    {
        _context = context;
        _addresses = _context.Set<Address>();
    }
}
