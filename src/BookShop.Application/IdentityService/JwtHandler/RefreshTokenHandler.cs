using System.Security.Cryptography;
using System.Text;
using BookShop.Application.IdentityService.Jwt;
using BookShop.DataAccess.Entities;

namespace BookShop.Application.IdentityService.JwtHandler;

/// <summary>
///     Implementation refresh token generator interface.
/// </summary>
public class RefreshTokenHandler : IRefreshTokenHandler
{
    public string Generate(int length)
    {
        const string Chars =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz!@#$%^&*+=";

        if (length < RefreshToken.MetaData.RefreshTokenValue.MinLength)
        {
            return string.Empty;
        }

        StringBuilder builder = new();

        for (int time = default; time < length; time++)
        {
            builder.Append(
                value: Chars[index: RandomNumberGenerator.GetInt32(toExclusive: Chars.Length)]
            );
        }

        return builder.ToString();
    }
}
