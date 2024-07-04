namespace BookShop.Application.Features.Orders.GetAllTemporarilyRemovedOrder;

/// <summary>
///     Extension Method for GetAllTemporarilyRemovedOrder features.
/// </summary>
public static class GetAllTemporarilyRemovedOrderExtensionMethod
{
    public static string ToAppCode(this GetAllTemporarilyRemovedOrderResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllTemporarilyRemovedOrder)}Feature: {statusCode}";
    }
}
