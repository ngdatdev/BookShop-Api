using BookShop.Data.Features.Repositories.Payments.UpdatePaymentByWebHook;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Payments.UpdatePaymentByWebHook;

/// <summary>
///    Implement of IUpdatePaymentByWebHook repository.
/// </summary>
internal partial class UpdatePaymentByWebHookRepository : IUpdatePaymentByWebHookRepository
{
    private readonly BookShopContext _context;
    private DbSet<Payment> _payments;

    public UpdatePaymentByWebHookRepository(BookShopContext context)
    {
        _context = context;
        _payments = _context.Set<Payment>();
    }
}
