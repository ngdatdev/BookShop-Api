namespace BookShop.Application.Features.Auth.Login;

/// <summary>
///     Login Response Status Code
/// </summary>
public enum LoginResponseStatusCode
{
    USER_IS_NOT_FOUND,
    USER_IS_LOCKED_OUT,
    USER_PASSWORD_IS_NOT_CORRECT,
    USER_IS_TEMPORARILY_REMOVED,
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    EMAIL_IS_NOT_CONFIRMED
}
