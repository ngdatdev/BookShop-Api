namespace BookShop.API.Controllers.Payments.CreatePaymentLink.HttpResponseMapper;

/// <summary>
///     CreatePaymentLink extension method
/// </summary>
internal static class CreatePaymentLinkHttpResponseMapper
{
    private static CreatePaymentLinkHttpResponseManager _CreatePaymentLinkHttpResponseManager;

    internal static CreatePaymentLinkHttpResponseManager Get()
    {
        return _CreatePaymentLinkHttpResponseManager ??= new();
    }
}
