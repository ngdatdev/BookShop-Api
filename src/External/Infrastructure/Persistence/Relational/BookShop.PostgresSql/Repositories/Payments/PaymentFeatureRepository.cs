using BookShop.Data.Features.Repositories.Payments;
using BookShop.Data.Features.Repositories.Payments.GetAllPayments;
using BookShop.Data.Features.Repositories.Payments.GetPaymentsByMethod;
using BookShop.Data.Features.Repositories.Payments.UpdatePaymentCOD;
using BookShop.PostgresSql.Data;
using BookShop.PostgresSql.Repositories.Payments.GetAllPayments;
using BookShop.PostgresSql.Repositories.Payments.GetPaymentsByMethod;
using BookShop.PostgresSql.Repositories.Payments.UpdatePaymentCOD;

namespace BookShop.PostgresSql.Repositories.Payments;

/// <summary>
///    Implement of PaymentFeatureRepository interface.
/// </summary>
internal class PaymentFeatureRepository : IPaymentFeatureRepository
{
    private readonly BookShopContext _context;

    private IGetAllPaymentsRepository _getAllPaymentsRepository;
    private IGetPaymentsByMethodRepository _getPaymentsByMethodRepository;
    private IUpdatePaymentCODRepository _updatePaymentCODRepository;

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

    public IGetPaymentsByMethodRepository GetPaymentsByMethodRepository
    {
        get
        {
            return _getPaymentsByMethodRepository ??= new GetPaymentsByMethodRepository(
                context: _context
            );
        }
    }

    public IUpdatePaymentCODRepository UpdatePaymentCODRepository
    {
        get
        {
            return _updatePaymentCODRepository ??= new UpdatePaymentCODRepository(
                context: _context
            );
        }
    }
}
