using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.Addresses.RemoveAddressPermanentlyRemovedById;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Addresses.RemoveAddressPermanentlyRemovedById;

/// <summary>
///    Implement of IRemoveAddressPermanentlyRemovedByIdRepository repository.
/// </summary>
internal partial class RemoveAddressPermanentlyRemovedByIdRepository
    : IRemoveAddressPermanentlyRemovedByIdRepository
{
    private readonly BookShopContext _context;
    private DbSet<Address> _addresses;

    public RemoveAddressPermanentlyRemovedByIdRepository(BookShopContext context)
    {
        _context = context;
        _addresses = _context.Set<Address>();
    }
}
