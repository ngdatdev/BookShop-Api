using System.IO;
using System.Linq;
using BookShop.Application.Shared.Features;
using FluentValidation;

namespace BookShop.Application.Features.Reviews.GetReviewsByUserId;

/// <summary>
///    GetReviewsByUserId Request Validator
/// </summary>
public sealed class GetReviewsByUserIdRequestValidator
    : FeatureRequestValidator<GetReviewsByUserIdRequest, GetReviewsByUserIdResponse>
{
    public GetReviewsByUserIdRequestValidator()
    {
        RuleFor(expression: request => request.UserId).NotEmpty();
    }
}
