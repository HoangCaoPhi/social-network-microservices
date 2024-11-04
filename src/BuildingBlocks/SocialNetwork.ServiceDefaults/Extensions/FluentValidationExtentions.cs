using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Results;
using SharpGrip.FluentValidation.AutoValidation.Shared.Extensions;
using System.Net;

namespace SocialNetwork.ServiceDefaults.Extensions;

public static class FluentValidationExtentions
{
    public static IServiceCollection AddFluentValidationValidation(this IHostApplicationBuilder builder)
    {
        builder.Services.AddFluentValidationAutoValidation(configuration =>
        {
            configuration.OverrideDefaultResultFactoryWith<CustomResultFactory>();
        });

        return builder.Services;
    }

    public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
    {
        public IResult CreateResult(EndpointFilterInvocationContext context, ValidationResult validationResult)
        {
            var validationProblemErrors = validationResult.ToValidationProblemErrors();
           
            return Results.ValidationProblem(validationProblemErrors,
                "One or more validation errors has occurred",
                "ValidationFailure", 
                (int)HttpStatusCode.BadRequest,
                "Validation error");
        }
    }
}
