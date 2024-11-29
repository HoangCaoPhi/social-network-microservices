using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Shared.Identity;
public class IdentityService(IHttpContextAccessor httpContext) : IIdentityService
{
    public string? GetUserIdentity() =>
        httpContext.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    public string? GetFullName() =>
        httpContext.HttpContext?.User.FindFirst("Name")?.Value ??
        httpContext.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value;


    public string? GetEmail() =>
        httpContext.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

    public bool IsAdminRole() =>
        httpContext.HttpContext?.User.IsInRole("Admin") ?? false;
}
