namespace BookShop.Application.Features.Payments.UpdatePaymentCOD;

/// <summary>
///     UpdatePaymentCOD Response Status Code
/// </summary>
public enum UpdatePaymentCODResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    PAYMENT_ID_IS_NOT_FOUND,
    PAYMENT_TEMPORARILY_REMOVED,
}
