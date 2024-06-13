using FluentValidation;

namespace BookShop.Application.Shared.Features;

/// <summary>
///     Abstract for feature request validators.
/// </summary>
public abstract class FeatureRequestValidator<TRequest, TResponse> : AbstractValidator<TRequest>
    where TRequest : class, IFeatureRequest<TResponse>
    where TResponse : class, IFeatureResponse { }
