namespace BookShop.Application.Features.Roles.RemoveRoleTemporarilyById;

/// <summary>
///     RemoveRoleTemporarilyById Response Status Code
/// </summary>
public enum RemoveRoleTemporarilyByIdResponseStatusCode
{
    ROLE_IS_NOT_FOUND,
    ROLE_IS_ALREADY_TEMPORARILY_REMOVED,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
}
