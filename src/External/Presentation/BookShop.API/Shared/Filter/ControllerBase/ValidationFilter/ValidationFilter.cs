using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using BookShop.API.AppCodes;
using BookShop.API.CommonResponse;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookShop.API.Shared.Filter.ControllerBase.ValidationFilter;

/// <summary>
//      ValidationFilter is responsible for validating incoming requests.
/// </summary>
public class ValidationFilter<TRequest> : IAsyncActionFilter
{
    private readonly IValidator<TRequest> _validator;

    public ValidationFilter(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var argument = context.ActionArguments.SingleOrDefault(arg => arg.Value is TRequest);
        if (!(argument.Value is TRequest model))
        {
            SetErrorResponse(context.HttpContext, "Invalid request type");
            return;
        }

        var validationResult = _validator.Validate(model);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            SetErrorResponse(context.HttpContext, string.Join(", ", errors));
        }

        await next();
    }

    private static void SetErrorResponse(HttpContext httpContext, string errorMessage)
    {
        httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        var response = new ApiResponse
        {
            AppCode = CommonAppCode.INPUT_VALIDATION_FAIL.ToString().Replace("_", " "),
            ErrorMessages = [errorMessage]
        };
        var jsonResponse = JsonSerializer.Serialize(response);
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        httpContext.Response.WriteAsync(jsonResponse);
    }
}
