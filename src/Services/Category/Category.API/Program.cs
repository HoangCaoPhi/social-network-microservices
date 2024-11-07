using Asp.Versioning;
using Category.API;
using Category.API.Extensions;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using SocialNetwork.ServiceDefaults.Extensions;
using SocialNetwork.ServiceDefaults.Filters;
using Web.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);  
        options.AssumeDefaultVersionWhenUnspecified = true;  
        options.ReportApiVersions = true; 
        options.ApiVersionReader = ApiVersionReader.Combine( 
            new HeaderApiVersionReader("X-Api-Version")  
        );
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    }); ;
 
builder.Services.AddMinimalEndpoints(typeof(IAssemblyMarker).Assembly);
builder.AddApplicationServices();
 
var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();  
}

var apiVersionSet = app
    .NewApiVersionSet()
    .HasApiVersion(new(1, 0))
    .ReportApiVersions()
    .Build();

var groupBuilder = app
    .MapGroup("/api")
    .WithApiVersionSet(apiVersionSet)
    .AddFluentValidationAutoValidation()
    .AddEndpointFilter<ApiResponseFilter>()
    .RequireAuthorization();

app.MapMinimalEndpoints(groupBuilder);

app.UseHttpsRedirection();

app.Run();
 