using GetMyTicket.Common.ErrorHandling;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GetMyTicket.API.ExceptionHandler
{
    internal sealed class ExceptionHandler
        : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            httpContext.Response.StatusCode = exception switch
            {
                ApplicationError => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Title = "An error occured",
                Detail = exception.Message,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
                Status = httpContext.Response.StatusCode
            }, cancellationToken: cancellationToken);

            return true;
        }
    }
}
