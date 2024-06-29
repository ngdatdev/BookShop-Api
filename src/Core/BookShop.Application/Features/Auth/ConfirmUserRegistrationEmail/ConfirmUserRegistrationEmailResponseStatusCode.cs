namespace BookShop.Application.Features.Auth.ConfirmUserRegistrationEmail;

/// <summary>
///     ConfirmUserRegistrationEmail Response Status Code
/// </summary>
public enum ConfirmUserRegistrationEmailResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    USER_HAS_CONFIRMED_REGISTRATION_EMAIL,
    USER_IS_TEMPORARILY_REMOVED,
    USER_IS_NOT_FOUND,
    TOKEN_IS_NOT_CORRECT,
}
