using System.Threading.Tasks;

namespace BookShop.Application.Shared.PaymentGateway;

/// <summary>
///     Represent interface of payment gateway handler.
/// </summary>
public interface IPaymentHandler
{
    /// <summary>
    ///     Create payment link url.
    /// </summary>
    /// <param name="paymentData">
    ///     Model contains payment information.
    /// </param>
    /// <returns>
    ///     String contain checkout url.
    /// </returns>
    Task<string> CreatePaymentLink(PaymentModel paymentData);
}
