namespace BookShop.Application.Features.Auth.RefreshAccessToken;

/// <summary>
///     RefreshAccessToken Response Status Code
/// </summary>
public enum RefreshAccessTokenResponseStatusCode
{
    REFRESH_TOKEN_IS_NOT_FOUND,
    REFRESH_TOKEN_IS_EXPIRED,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
}
