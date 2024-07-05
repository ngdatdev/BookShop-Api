namespace BookShop.Application.Features.Roles.UpdateRoleById;

/// <summary>
///     UpdateRoleById Response Status Code
/// </summary>
public enum UpdateRoleByIdResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    ROLE_IS_TEMPORARILY_REMOVED,
    ROLE_NAME_IS_ALREADY_EXISTS,
    ROLE_IS_NOT_FOUND
}
