namespace BookShop.API.Controllers.Payments.UpdatePaymentCOD.HttpResponseMapper;

/// <summary>
///     UpdatePaymentCOD extension method
/// </summary>
internal static class UpdatePaymentCODHttpResponseMapper
{
    private static UpdatePaymentCODHttpResponseManager _UpdatePaymentCODHttpResponseManager;

    internal static UpdatePaymentCODHttpResponseManager Get()
    {
        return _UpdatePaymentCODHttpResponseManager ??= new();
    }
}
