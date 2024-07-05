namespace BookShop.Application.Features.OrderDetails.GetAllTemporarilyRemovedOrderDetails;

/// <summary>
///     Extension Method for GetAllTemporarilyRemovedOrderDetails features.
/// </summary>
public static class GetAllTemporarilyRemovedOrderDetailsExtensionMethod
{
    public static string ToAppCode(
        this GetAllTemporarilyRemovedOrderDetailsResponseStatusCode statusCode
    )
    {
        return $"{nameof(GetAllTemporarilyRemovedOrderDetails)}Feature: {statusCode}";
    }
}
