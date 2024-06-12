using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BookShop.API.AppCodes;
using BookShop.API.CommonResponse;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookShop.API.Shared.Filter.ValidationRequestFilter;

/// <summary>
//      ValidationFilter is responsible for validating incoming requests.
/// </summary>
public class ValidationRequestFilter<TRequest> : IAsyncActionFilter
{
    private readonly IValidator<TRequest> _validator;

    public ValidationRequestFilter(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var argument = context.ActionArguments.SingleOrDefault(arg => arg.Value is TRequest);
        if (argument.Value is not TRequest model)
        {
            await next();
            return;
        }

        var validationResult = await _validator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToArray();
            SetErrorResponse(context.HttpContext, errors);
            context.Result = new BadRequestObjectResult(errors);
            return;
        }

        await next();
    }

    private static void SetErrorResponse(HttpContext httpContext, IEnumerable<string> errorMessage)
    {
        httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        var response = new ApiResponse
        {
            AppCode = CommonAppCode.INPUT_VALIDATION_FAIL.ToString().Replace("_", " "),
            ErrorMessages = errorMessage
        };
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        httpContext.Response.WriteAsJsonAsync(response);
    }
}
