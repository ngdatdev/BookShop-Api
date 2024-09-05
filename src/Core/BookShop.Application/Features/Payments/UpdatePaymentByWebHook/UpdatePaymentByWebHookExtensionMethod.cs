namespace BookShop.Application.Features.Payments.UpdatePaymentByWebHook;

/// <summary>
///     Extension Method for UpdatePaymentByWebHook features.
/// </summary>
public static class UpdatePaymentByWebHookExtensionMethod
{
    public static string ToAppCode(this UpdatePaymentByWebHookResponseStatusCode statusCode)
    {
        return $"{nameof(UpdatePaymentByWebHook)}Feature: {statusCode}";
    }
}
