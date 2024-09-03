namespace BookShop.Application.Features.Payments.GetPaymentsByMethod;

/// <summary>
///     Extension Method for GetPaymentsByMethod features.
/// </summary>
public static class GetPaymentsByMethodExtensionMethod
{
    public static string ToAppCode(this GetPaymentsByMethodResponseStatusCode statusCode)
    {
        return $"{nameof(GetPaymentsByMethod)}Feature: {statusCode}";
    }
}
