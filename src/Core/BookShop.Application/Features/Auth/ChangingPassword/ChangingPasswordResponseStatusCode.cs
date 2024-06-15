namespace BookShop.Application.Features.Auth.ChangingPassword;

/// <summary>
///     ChangingPassword Response Status Code
/// </summary>
public enum ChangingPasswordResponseStatusCode
{
    NEW_PASSWORD_IS_NOT_VALID,
    RESET_PASSWORD_TOKEN_IS_NOT_FOUND,
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    USER_IS_TEMPORARILY_REMOVED
}
