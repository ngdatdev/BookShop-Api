namespace BookShop.Application.Features.Roles.RestoreRoleById;

/// <summary>
///     RestoreRoleById Response Status Code
/// </summary>
public enum RestoreRoleByIdResponseStatusCode
{
    ROLE_IS_NOT_FOUND,
    ROLE_IS_NOT_TEMPORARILY_REMOVED,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
}
