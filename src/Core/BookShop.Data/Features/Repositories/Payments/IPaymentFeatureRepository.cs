using BookShop.Data.Features.Repositories.Payments.GetAllPayments;
using BookShop.Data.Features.Repositories.Payments.GetPaymentsByMethod;

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
}
