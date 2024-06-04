using BookShop.Application.Shared.Features;
using BookShop.Data.Shared.Entities;
using FluentValidation;

namespace BookShop.Application.Features.HelloWorld;

/// <summary>
///     Hello world Request Validator
/// </summary>
public sealed class HelloWorldRequestValidator
    : FeatureRequestValidator<HelloWorldRequest, HelloWorldResponse>
{
    public HelloWorldRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.Name)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(minimumLength: User.MetaData.UserName.MinLength);
    }
}
