namespace BookShop.API.Controllers.Payments.GetPaymentsByMethod.HttpResponseMapper;

/// <summary>
///     GetPaymentsByMethod extension method
/// </summary>
internal static class GetPaymentsByMethodHttpResponseMapper
{
    private static GetPaymentsByMethodHttpResponseManager _GetPaymentsByMethodHttpResponseManager;

    internal static GetPaymentsByMethodHttpResponseManager Get()
    {
        return _GetPaymentsByMethodHttpResponseManager ??= new();
    }
}
