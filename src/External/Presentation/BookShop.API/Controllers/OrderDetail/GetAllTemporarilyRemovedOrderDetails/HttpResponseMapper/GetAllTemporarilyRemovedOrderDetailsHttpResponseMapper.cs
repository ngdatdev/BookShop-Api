namespace BookShop.API.Controllers.OrderDetail.GetAllTemporarilyRemovedOrderDetails.HttpResponseMapper;

/// <summary>
///     GetAllTemporarilyRemovedOrderDetails extension method
/// </summary>
internal static class GetAllTemporarilyRemovedOrderDetailsHttpResponseMapper
{
    private static GetAllTemporarilyRemovedOrderDetailsHttpResponseManager _GetAllTemporarilyRemovedOrderDetailsHttpResponseManager;

    internal static GetAllTemporarilyRemovedOrderDetailsHttpResponseManager Get()
    {
        return _GetAllTemporarilyRemovedOrderDetailsHttpResponseManager ??= new();
    }
}
