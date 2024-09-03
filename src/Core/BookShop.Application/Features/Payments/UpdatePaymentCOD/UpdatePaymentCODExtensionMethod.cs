namespace BookShop.Application.Features.Payments.UpdatePaymentCOD;

/// <summary>
///     Extension Method for UpdatePaymentCOD features.
/// </summary>
public static class UpdatePaymentCODExtensionMethod
{
    public static string ToAppCode(this UpdatePaymentCODResponseStatusCode statusCode)
    {
        return $"{nameof(UpdatePaymentCOD)}Feature: {statusCode}";
    }
}
