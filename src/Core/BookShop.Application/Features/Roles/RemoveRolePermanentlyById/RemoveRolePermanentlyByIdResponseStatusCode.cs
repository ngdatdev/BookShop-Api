namespace BookShop.Application.Features.Roles.RemoveRolePermanentlyById;

/// <summary>
///     RemoveRolePermanentlyById Response Status Code
/// </summary>
public enum RemoveRolePermanentlyByIdResponseStatusCode
{
    ROLE_IS_NOT_FOUND,
    ROLE_IS_NOT_TEMPORARILY_REMOVED,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
}
