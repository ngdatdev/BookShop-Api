namespace BookShop.Application.Features.Payments.UpdatePaymentByWebHook;

/// <summary>
///     UpdatePaymentByWebHook Response Status Code
/// </summary>
public enum UpdatePaymentByWebHookResponseStatusCode
{
    OPERATION_SUCCESS,
    SIGNATURE_IS_NOT_VALID,
    WEBHOOK_RETURN_ERROR,
    ORDER_IS_NOT_FOUND,
    DATABASE_OPERATION_FAIL
}
