using BookShop.Data.Features.Repositories.Payments.GetAllPayments;
using BookShop.Data.Features.Repositories.Payments.GetPaymentsByMethod;
using BookShop.Data.Features.Repositories.Payments.UpdatePaymentByWebHook;
using BookShop.Data.Features.Repositories.Payments.UpdatePaymentCOD;

namespace BookShop.Data.Features.Repositories.Payments;

/// <summary>
///     Interface for payment repository manager.
/// </summary>
public interface IPaymentFeatureRepository
{
    /// <summary>
    ///     Gets create order feature repository.
    /// </summary>
    public IGetAllPaymentsRepository GetAllPaymentsRepository { get; }

    /// <summary>
    ///     Gets get payments by method feature repository.
    /// </summary>
    public IGetPaymentsByMethodRepository GetPaymentsByMethodRepository { get; }

    /// <summary>
    ///     Gets update payments by orderId feature repository.
    /// </summary>
    public IUpdatePaymentCODRepository UpdatePaymentCODRepository { get; }

    /// <summary>
    ///     Gets update payments from webhook feature repository.
    /// </summary>
    public IUpdatePaymentByWebHookRepository UpdatePaymentByWebHookRepository { get; }
}
