using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Data.Features.Repositories.Addresses.RemoveAddressTemporarilyRemovedById;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Addresses.RemoveAddressTemporarilyRemovedById;

/// <summary>
///    Implement of IRemoveAddressTemporarilyRemovedByIdRepository repository.
/// </summary>
internal partial class RemoveAddressTemporarilyRemovedByIdRepository
    : IRemoveAddressTemporarilyRemovedByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<Address> _addresses;

    public RemoveAddressTemporarilyRemovedByIdRepository(BookShopContext context)
    {
        _context = context;
        _addresses = _context.Set<Address>();
    }
}
