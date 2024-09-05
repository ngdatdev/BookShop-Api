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

    public bool VerifyWebhookData(Application.Shared.PaymentGateway.WebhookType webhookType)
    {
        var webhookTypeInstance = new Net.payOS.Types.WebhookType(
            webhookType.Code,
            webhookType.Desc,
            new WebhookData(
                webhookType.Data.AccountNumber,
                webhookType.Data.Amount,
                webhookType.Data.Description,
                webhookType.Data.Reference,
                webhookType.Data.TransactionDateTime,
                webhookType.Data.VirtualAccountNumber,
                webhookType.Data.CounterAccountBankId,
                webhookType.Data.CounterAccountBankName,
                webhookType.Data.CounterAccountName,
                webhookType.Data.CounterAccountNumber,
                webhookType.Data.VirtualAccountName,
                webhookType.Data.OrderCode,
                webhookType.Data.Currency,
                webhookType.Data.PaymentLinkId,
                webhookType.Data.Code,
                webhookType.Data.Desc
            ),
            webhookType.Signature
        );

        WebhookData webhookData;

        webhookData = _payOS.verifyPaymentWebhookData(webhookTypeInstance);

        if (webhookData != null)
        {
            return true;
        }

        return false;
    }
}
