namespace BookShop.Application.Features.Payments.GetAllPayments;

/// <summary>
///     Extension Method for GetAllPayments features.
/// </summary>
public static class GetAllPaymentsExtensionMethod
{
    public static string ToAppCode(this GetAllPaymentsResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllPayments)}Feature: {statusCode}";
    }
}
