using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Data.Features.Repositories.Addresses.UpdateAddressById;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Addresses.UpdateAddressById;

/// <summary>
///    Implement of IUpdateAddressByIdRepository repository.
/// </summary>
internal partial class UpdateAddressByIdRepository : IUpdateAddressByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<Address> _addresses;

    public UpdateAddressByIdRepository(BookShopContext context)
    {
        _context = context;
        _addresses = _context.Set<Address>();
    }
}
