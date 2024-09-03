using BookShop.Data.Features.Repositories.Payments.GetAllPayments;

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
}
