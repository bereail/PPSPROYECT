using System.Net;

namespace MiniMarket_API.Middlewares
{
    public class ExceptionHandler
    {
        private readonly ILogger logger;
        private readonly RequestDelegate next;

        public ExceptionHandler(ILogger<ExceptionHandler> logger,
            RequestDelegate next) 
        { 
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                // Assigning a unique ID to our Exception
                var errorId = Guid.NewGuid();
                // Logging the Exception
                logger.LogError(ex, $"{errorId} : {ex.Message} ");

                // Returning the Exception with a custom message
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something Went Wrong!"
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
