using FC.AbreuBarber.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fc.AbreuBarber.Api.Filters
{
    public class ApiGlobalExpectionFilter : IExceptionFilter
    {

        private readonly IHostEnvironment _env;
        public ApiGlobalExpectionFilter(IHostEnvironment env) {
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            var details = new ProblemDetails();

            var expection = context.Exception;

            if(_env.IsDevelopment())
            {
                details.Extensions.Add("StackTrace", expection.StackTrace);
            }


            if(expection is EntityValidationException)
            {
                var ex = expection as EntityValidationException;

                details.Title = "One or more validation errors ocurred";
                details.Status = StatusCodes.Status422UnprocessableEntity;
                details.Type = "UnprocessableEntity";
                details.Detail = ex!.Message;
            } else
            {
                details.Title = "An unexpected error ocurred";
                details.Status = StatusCodes.Status422UnprocessableEntity;
                details.Type = "UnexpectedError";
                details.Detail = expection.Message;
            }

            context.HttpContext.Response.StatusCode = (int)details.Status;
            context.Result = new ObjectResult(details);
            context.ExceptionHandled = true;
        }
    }
}
