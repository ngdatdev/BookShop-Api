using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.Addresses.RestoreAddressById;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Addresses.RestoreAddressById;

/// <summary>
///    Implement of IRestoreAddressByIdRepository repository.
/// </summary>
internal partial class RestoreAddressByIdRepository : IRestoreAddressByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<Address> _addresses;

    public RestoreAddressByIdRepository(BookShopContext context)
    {
        _context = context;
        _addresses = _context.Set<Address>();
    }
}
