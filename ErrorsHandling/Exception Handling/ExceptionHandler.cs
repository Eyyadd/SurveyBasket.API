using Microsoft.AspNetCore.Diagnostics;

namespace SurveyBasket.API.ErrorsHandling.Exception_Handling
{
    public class ExceptionHandler : IExceptionHandler
    {

        
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {

            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Type = "https://datatracker.ietf.org/doc/html/rfc2616#section-10.5.1",
                Title = exception.Message,
            };

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
            return true;
        }
    }
}
