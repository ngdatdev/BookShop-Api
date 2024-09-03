namespace BookShop.API.Controllers.Payments.GetAllPayments.HttpResponseMapper;

/// <summary>
///     GetAllPayments extension method
/// </summary>
internal static class GetAllPaymentsHttpResponseMapper
{
    private static GetAllPaymentsHttpResponseManager _GetAllPaymentsHttpResponseManager;

    internal static GetAllPaymentsHttpResponseManager Get()
    {
        return _GetAllPaymentsHttpResponseManager ??= new();
    }
}
