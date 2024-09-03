using BookShop.Data.Features.Repositories.Payments.UpdatePaymentCOD;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Payments.UpdatePaymentCOD;

/// <summary>
///    Implement of IUpdatePaymentCOD repository.
/// </summary>
internal partial class UpdatePaymentCODRepository : IUpdatePaymentCODRepository
{
    private readonly BookShopContext _context;
    private DbSet<Payment> _payments;

    public UpdatePaymentCODRepository(BookShopContext context)
    {
        _context = context;
        _payments = _context.Set<Payment>();
    }
}
