using BookShop.Data.Features.Repositories.Payments.GetAllPayments;
using BookShop.Data.Shared.Entities;
using BookShop.PostgresSql.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.PostgresSql.Repositories.Payments.GetAllPayments;

/// <summary>
///    Implement of IGetAllPayments repository.
/// </summary>
internal partial class GetAllPaymentsRepository : IGetAllPaymentsRepository
{
    private readonly BookShopContext _context;
    private DbSet<Payment> _payments;

    public GetAllPaymentsRepository(BookShopContext context)
    {
        _context = context;
        _payments = _context.Set<Payment>();
    }
}
