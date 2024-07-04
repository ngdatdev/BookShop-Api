namespace BookShop.API.Controllers.Order.GetAllTemporarilyRemovedOrder.HttpResponseMapper;

/// <summary>
///     GetAllTemporarilyRemovedOrder extension method
/// </summary>
internal static class GetAllTemporarilyRemovedOrderHttpResponseMapper
{
    private static GetAllTemporarilyRemovedOrderHttpResponseManager _GetAllTemporarilyRemovedOrderHttpResponseManager;

    internal static GetAllTemporarilyRemovedOrderHttpResponseManager Get()
    {
        return _GetAllTemporarilyRemovedOrderHttpResponseManager ??= new();
    }
}
