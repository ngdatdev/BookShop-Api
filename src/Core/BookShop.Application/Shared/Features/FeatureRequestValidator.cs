using FluentValidation;

namespace BookShop.Application.Shared.Features;

public abstract class FeatureRequestValidator<TRequest, TResponse> : AbstractValidator<TRequest>
    where TRequest : class, IFeatureRequest<TResponse>
    where TResponse : class, IFeatureResponse { }
