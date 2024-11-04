using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SocialNetwork.ServiceDefaults.ErrorHandling;
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        ProblemDetails problemDetails = CreateProblemDetailFromException(exception);

        httpContext.Response.StatusCode = problemDetails.Status!.Value;

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private static ProblemDetails CreateProblemDetailFromException(Exception exception)
    {
        return exception switch
        {
            ValidationException validationException => new ValidationProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "ValidationFailure",
                Title = "Validation error",
                Detail = "One or more validation errors has occurred",
                Errors = validationException.Errors
                .ToDictionary(
                    error => error.PropertyName,
                    error => new[] { error.ErrorMessage }
                )
            },
            _ => new ProblemDetails
            {
                Type = "ServerError",
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server error",
                Detail = "Server error"
            }
        };
    }
}
