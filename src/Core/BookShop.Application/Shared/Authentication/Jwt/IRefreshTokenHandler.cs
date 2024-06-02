namespace BookShop.Application.Shared.Authentication.Jwt;

/// <summary>
///     Represent refresh token generator interface.
/// </summary>
public interface IRefreshTokenHandler
{
    /// <summary>
    ///     Generate jwt base on list of claims.
    /// </summary>
    /// <param name="length">
    ///     Length of refresh token.
    /// </param>
    /// <returns>
    ///     A string contain refresh token
    /// </returns>
    public string Generate(int length);
}
