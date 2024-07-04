using BookShop.Application.Shared.Features;
using FluentValidation;

namespace BookShop.Application.Features.Orders.RemoveOrderTemporarilyById;

/// <summary>
///    RemoveOrderTemporarilyById Request Validator
/// </summary>
public sealed class RemoveOrderTemporarilyByIdRequestValidator
    : FeatureRequestValidator<RemoveOrderTemporarilyByIdRequest, RemoveOrderTemporarilyByIdResponse>
{
    public RemoveOrderTemporarilyByIdRequestValidator()
    {
        RuleFor(request => request.OrderId).NotEmpty();
    }
}
