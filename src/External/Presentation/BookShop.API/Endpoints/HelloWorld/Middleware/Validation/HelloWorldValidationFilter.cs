using System.Linq;
using System.Threading.Tasks;
using BookShop.Application.Features.HelloWorld;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookShop.API.Endpoints.HelloWorld.Middleware.Validation
{
    public class HelloWorldValidationFilter : IAsyncActionFilter
    {
        private readonly IValidator<HelloWorldRequest> _validator;

        public HelloWorldValidationFilter(IValidator<HelloWorldRequest> validator)
        {
            _validator = validator;
        }

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next
        )
        {
            var argument = context.ActionArguments.SingleOrDefault(arg =>
                arg.Value is HelloWorldRequest
            );

            if (argument.Value is HelloWorldRequest model)
            {
                var validationResult = await _validator.ValidateAsync(model);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult
                        .Errors.Select(error => error.ErrorMessage)
                        .ToList();

                    context.Result = new BadRequestObjectResult(errors);
                    return;
                }
            }

            await next();
        }
    }
}
