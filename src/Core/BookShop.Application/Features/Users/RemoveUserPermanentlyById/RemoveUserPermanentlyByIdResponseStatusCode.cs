namespace BookShop.Application.Features.Users.RemoveUserPermanentlyById;

/// <summary>
///     RemoveUserPermanentlyById Response Status Code
/// </summary>
public enum RemoveUserPermanentlyByIdResponseStatusCode
{
    OPERATION_SUCCESS,
    USER_IS_NOT_TEMPORARILY_REMOVED,
    USER_ID_IS_NOT_FOUND,
    DATABASE_OPERATION_FAIL,
}
