using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Data.Features.Repositories.Payments.GetPaymentsByMethod;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Payments.GetPaymentsByMethod;

/// <summary>
///    Implement of IGetPaymentsByMethod repository.
/// </summary>
internal partial class GetPaymentsByMethodRepository : IGetPaymentsByMethodRepository
{
    private readonly BookShopContext _context;
    private DbSet<Payment> _payments;

    public GetPaymentsByMethodRepository(BookShopContext context)
    {
        _context = context;
        _payments = _context.Set<Payment>();
    }
}
