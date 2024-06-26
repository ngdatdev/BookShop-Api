using BookShop.Application.Shared.Features;
using FluentValidation;

namespace BookShop.Application.Features.Users.RemoveUserPermanentlyById;

/// <summary>
///    RemoveUserPermanentlyById Request Validator
/// </summary>
public sealed class RemoveUserPermanentlyByIdRequestValidator
    : FeatureRequestValidator<RemoveUserPermanentlyByIdRequest, RemoveUserPermanentlyByIdResponse>
{
    public RemoveUserPermanentlyByIdRequestValidator()
    {
        RuleFor(request => request.UserId).NotEmpty();
    }
}
