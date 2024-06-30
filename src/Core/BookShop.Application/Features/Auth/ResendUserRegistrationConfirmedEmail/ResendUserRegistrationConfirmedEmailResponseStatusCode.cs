namespace BookShop.Application.Features.Auth.ResendUserRegistrationConfirmedEmail;

/// <summary>
///     ResendUserRegistrationConfirmedEmail Response Status Code
/// </summary>
public enum ResendUserRegistrationConfirmedEmailResponseStatusCode
{
    USER_IS_NOT_FOUND,
    USER_HAS_CONFIRMED_EMAIL,
    OPERATION_SUCCESS,
    USER_IS_TEMPORARILY_REMOVED,
    SENDING_USER_CONFIRMATION_MAIL_FAIL
}
