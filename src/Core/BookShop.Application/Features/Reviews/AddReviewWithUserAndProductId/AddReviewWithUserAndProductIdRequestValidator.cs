using System.IO;
using System.Linq;
using BookShop.Application.Shared.Features;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace BookShop.Application.Features.Reviews.AddReviewWithUserAndProductId;

/// <summary>
///    AddReviewWithUserAndProductId Request Validator
/// </summary>
public sealed class AddReviewWithUserAndProductIdRequestValidator
    : FeatureRequestValidator<
        AddReviewWithUserAndProductIdRequest,
        AddReviewWithUserAndProductIdResponse
    >
{
    public AddReviewWithUserAndProductIdRequestValidator()
    {
        RuleFor(expression: request => request.Comment)
            .NotEmpty()
            .MinimumLength(minimumLength: Data.Shared.Entities.Review.MetaData.Comment.MinLength)
            .MaximumLength(maximumLength: Data.Shared.Entities.Review.MetaData.Comment.MaxLength);
    }
}
