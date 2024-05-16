using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MiniMarket_API.Model.Exceptions;

namespace MiniMarket_API.Application.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var statusCode = context.Exception switch
            {
                NotFoundException => StatusCodes.Status404NotFound,

                ValidationException => StatusCodes.Status400BadRequest,

                UnauthenticatedException => StatusCodes.Status401Unauthorized,

                _ => StatusCodes.Status500InternalServerError
            };
            
            // Making sure that 500 exceptions are dealt with by the Handler, instead of the Filters.
            if (statusCode != StatusCodes.Status500InternalServerError)

            context.Result = new ObjectResult(new
            {
                error = context.Exception.Message,
            })
            {
                StatusCode = statusCode
            };
        }
    }
}
