namespace BookShop.API.Controllers.Payments.UpdatePaymentByWebHook.HttpResponseMapper;

/// <summary>
///     UpdatePaymentByWebHook extension method
/// </summary>
internal static class UpdatePaymentByWebHookHttpResponseMapper
{
    private static UpdatePaymentByWebHookHttpResponseManager _UpdatePaymentByWebHookHttpResponseManager;

    internal static UpdatePaymentByWebHookHttpResponseManager Get()
    {
        return _UpdatePaymentByWebHookHttpResponseManager ??= new();
    }
}
