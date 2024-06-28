using BookShop.Application.Shared.Features;
using FluentValidation;

namespace BookShop.Application.Features.Users.RemoveUserTemporarilyById;

/// <summary>
///    RemoveUserTemporarilyById Request Validator
/// </summary>
public sealed class RemoveUserTemporarilyByIdRequestValidator
    : FeatureRequestValidator<RemoveUserTemporarilyByIdRequest, RemoveUserTemporarilyByIdResponse>
{
    public RemoveUserTemporarilyByIdRequestValidator()
    {
        RuleFor(request => request.UserId).NotEmpty();
    }
}
