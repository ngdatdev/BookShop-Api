namespace BookShop.Application.Features.Roles.CreateRole;

/// <summary>
///     CreateRole Response Status Code
/// </summary>
public enum CreateRoleResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    ROLE_IS_ALREADY_TEMPORARILY_REMOVED,
    ROLE_ALREADY_EXISTS
}
