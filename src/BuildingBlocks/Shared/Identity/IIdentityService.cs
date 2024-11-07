namespace Shared;

public interface IIdentityService
{
    string? GetUserIdentity();

    string? GetFullName();

    string? GetEmail();

    bool IsAdminRole();
}
