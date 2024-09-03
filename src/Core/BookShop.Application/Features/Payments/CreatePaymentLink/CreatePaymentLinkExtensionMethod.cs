namespace BookShop.Application.Features.Payments.CreatePaymentLink;

/// <summary>
///     Extension Method for CreatePaymentLink features.
/// </summary>
public static class CreatePaymentLinkExtensionMethod
{
    public static string ToAppCode(this CreatePaymentLinkResponseStatusCode statusCode)
    {
        return $"{nameof(CreatePaymentLink)}Feature: {statusCode}";
    }
}
