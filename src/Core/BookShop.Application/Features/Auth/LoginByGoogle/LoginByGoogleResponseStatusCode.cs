namespace BookShop.Application.Features.Auth.LoginByGoogle;

/// <summary>
///     LoginByGoogle Response Status Code
/// </summary>
public enum LoginByGoogleResponseStatusCode
{
    USER_IS_TEMPORARILY_REMOVED,
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    UNAUTHORIZE,
    DATABASE_OPERATION_FAIL,
    EMAIL_IS_NOT_CONFIRMED
}
