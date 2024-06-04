using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Shared.Filter.ValidationFilter;

public class ValidationFilter<TRequest> : IEndpointFilter
    where TRequest : class
{
    private readonly IValidator<TRequest> _validator;

    public ValidationFilter(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    public async ValueTask<object> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next
    )
    {
        var request = context.GetArgument<TRequest>(0);

        var validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }

        return await next(context);
    }
}
