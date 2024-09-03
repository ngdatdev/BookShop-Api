using BookShop.Data.Features.Repositories.Payments;
using BookShop.Data.Features.Repositories.Payments.GetAllPayments;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.Payments.GetAllPayments;

namespace BookShop.PostgresSql.Repositories.Payments;

/// <summary>
///    Implement of PaymentFeatureRepository interface.
/// </summary>
internal class PaymentFeatureRepository : IPaymentFeatureRepository
{
    private readonly BookShopContext _context;

    private IGetAllPaymentsRepository _getAllPaymentsRepository;

    internal PaymentFeatureRepository(BookShopContext context)
    {
        _context = context;
    }

    public IGetAllPaymentsRepository GetAllPaymentsRepository
    {
        get
        {
            return _getAllPaymentsRepository ??= new GetAllPaymentsRepository(context: _context);
        }
    }
}
