using System.IO;
using System.Linq;
using BookShop.Application.Shared.Features;
using FluentValidation;

namespace BookShop.Application.Features.Reviews.UpdateReviewById;

/// <summary>
///    UpdateReviewById Request Validator
/// </summary>
public sealed class UpdateReviewByIdRequestValidator
    : FeatureRequestValidator<UpdateReviewByIdRequest, UpdateReviewByIdResponse>
{
    public UpdateReviewByIdRequestValidator()
    {
        RuleFor(expression: request => request.Comment)
            .NotEmpty()
            .MinimumLength(minimumLength: Data.Shared.Entities.Review.MetaData.Comment.MinLength)
            .MaximumLength(maximumLength: Data.Shared.Entities.Review.MetaData.Comment.MaxLength);

        RuleFor(expression: request => request.ReviewId).NotEmpty();
    }
}
