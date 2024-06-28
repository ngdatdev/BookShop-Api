namespace BookShop.Application.Features.Users.RemoveUserTemporarilyById;

/// <summary>
///     RemoveUserTemporarilyById Response Status Code
/// </summary>
public enum RemoveUserTemporarilyByIdResponseStatusCode
{
    OPERATION_SUCCESS,
    USER_IS_ALREADY_TEMPORARILY_REMOVED,
    USER_ID_IS_NOT_FOUND,
    DATABASE_OPERATION_FAIL,
}
