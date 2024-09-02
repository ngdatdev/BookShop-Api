using BookShop.Application.Shared.PaymentGateway;
using Microsoft.IdentityModel.Tokens;
using Net.payOS;
using Net.payOS.Types;

namespace BookShop.PayOSGateway.Handler;

/// <summary>
///     Implementation of payment handler interface.
/// </summary>
public class PayOSHandler : IPaymentHandler
{
    private readonly PayOS _payOS;

    public PayOSHandler(PayOS payOS)
    {
        _payOS = payOS;
    }

    public async Task<string> CreatePaymentLink(PaymentModel paymentModel)
    {
        PaymentData paymentData = new PaymentData(
            orderCode: paymentModel.OrderCode,
            amount: paymentModel.Amount,
            description: paymentModel.Description,
            items: paymentModel
                .Items.Select(x => new ItemData(x.Name, x.Price, x.Quantity))
                .ToList(),
            cancelUrl: paymentModel.CancelUrl,
            returnUrl: paymentModel.ReturnUrl
        );

        CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);

        if (createPayment.checkoutUrl.IsNullOrEmpty())
        {
            return default;
        }

        return createPayment.checkoutUrl;
    }
}
