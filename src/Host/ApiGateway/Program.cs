using Duende.Bff.Yarp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using React.Bff;
using Shared.Constants;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder
    .Services.AddBff()
    .AddRemoteApis();

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddBffExtensions();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "cookie";
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    options.DefaultSignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
}).AddCookie("cookie", options =>
{
    options.Cookie.Name = "__Host-bff";
    options.Cookie.SameSite = SameSiteMode.Strict;
}).AddKeycloakOpenIdConnect(
                    ServiceName.Keycloak,
                    realm: nameof(SocialNetwork),
                    options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.ClientId = IdentityConfig.Client.Gateway;

                        options.GetClaimsFromUserInfoEndpoint = true;
                        options.SaveTokens = true;

                        options.Scope.Clear();
                        options.Scope.Add("openid");
                        options.Scope.Add("profile");
                        options.Scope.Add("offline_access");

                        options.ResponseType = OpenIdConnectResponseType.Code;
                    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
 
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseBff();
app.UseAuthorization();
app.MapBffManagementEndpoints();
app.MapDefaultEndpoints();
app.MapBffReverseProxy();

app.MapGet("/api/auth/access-token", async (HttpContext context) =>
{
    var accessToken = await context.GetTokenAsync("access_token");
    return new
    {
        AccessToken = accessToken,
    };
})
.RequireAuthorization()
.AsBffApiEndpoint();

app.MapGet("/Test", () =>
{
    return "Test Endpoint";
});

// Comment this out to use the external api
app.MapGroup("/api/todos")
    .ToDoGroup()
    .RequireAuthorization()
    .AsBffApiEndpoint();
 
app.MapFallbackToFile("/index.html");

app.Run();

 