using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Payments.CreatePaymentLink;

/// <summary>
///     CreatePaymentLink Response
/// </summary>
public class CreatePaymentLinkResponse : IFeatureResponse
{
    public CreatePaymentLinkResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public string CheckoutUrl { get; init; }
    }
}
