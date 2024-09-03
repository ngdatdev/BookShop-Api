using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.PaymentGateway;
using Microsoft.IdentityModel.Tokens;
using static BookShop.Application.Shared.PaymentGateway.PaymentModel;

namespace BookShop.Application.Features.Payments.CreatePaymentLink;

/// <summary>
///     CreatePaymentLink Handler
/// </summary>
public class CreatePaymentLinkHandler
    : IFeatureHandler<CreatePaymentLinkRequest, CreatePaymentLinkResponse>
{
    private readonly IPaymentHandler _paymentHandler;

    public CreatePaymentLinkHandler(IPaymentHandler paymentHandler)
    {
        _paymentHandler = paymentHandler;
    }

    /// <summary>
    ///     Entry of new request handler.
    /// </summary>
    /// <param name="request">
    ///     Request model.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the response.
    /// </returns>
    public async Task<CreatePaymentLinkResponse> HandlerAsync(
        CreatePaymentLinkRequest request,
        CancellationToken cancellationToken
    )
    {
        PaymentModel model =
            new()
            {
                OrderCode = request.OrderCode,
                ReturnUrl = request.ReturnUrl,
                Amount = request.Amount,
                CancelUrl = request.CancelUrl,
                Description = request.Description,
                Items = request
                    .Items.Select(i => new ItemData()
                    {
                        Name = i.Name,
                        Price = i.Price,
                        Quantity = i.Quantity,
                    })
                    .ToList(),
            };

        // Get url from payos gateway api.
        var checkoutUrl = await _paymentHandler.CreatePaymentLink(paymentData: model);

        if (checkoutUrl.IsNullOrEmpty())
        {
            return new() { StatusCode = CreatePaymentLinkResponseStatusCode.CREATE_URL_FAIL, };
        }

        // Response successfully.
        return new CreatePaymentLinkResponse()
        {
            StatusCode = CreatePaymentLinkResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new() { CheckoutUrl = checkoutUrl, }
        };
    }
}
