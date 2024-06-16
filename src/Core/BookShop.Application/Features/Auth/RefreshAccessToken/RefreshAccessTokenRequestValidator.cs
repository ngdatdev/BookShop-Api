using BookShop.Application.Shared.Features;
using BookShop.Data.Shared.Entities;
using FluentValidation;

namespace BookShop.Application.Features.Auth.RefreshAccessToken;

/// <summary>
///     RefreshAccessToken Request Validator
/// </summary>
public sealed class RefreshAccessTokenRequestValidator
    : FeatureRequestValidator<RefreshAccessTokenRequest, RefreshAccessTokenResponse>
{
    public RefreshAccessTokenRequestValidator()
    {
        RuleFor(expression: request => request.RefreshToken)
            .NotEmpty()
            .MinimumLength(minimumLength: RefreshToken.MetaData.RefreshTokenValue.MinLength);
    }
}
