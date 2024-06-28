namespace BookShop.Application.Features.Users.RestoreUserById;

/// <summary>
///     RestoreUserById Response Status Code
/// </summary>
public enum RestoreUserByIdResponseStatusCode
{
    OPERATION_SUCCESS,
    USER_IS_ALREADY_TEMPORARILY_REMOVED,
    USER_ID_IS_NOT_FOUND,
    DATABASE_OPERATION_FAIL,
}
