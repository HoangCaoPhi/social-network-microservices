namespace Shared.Identity;
public class TempIdentityService : IIdentityService
{
    public string? GetEmail()
    {
        return "phihoang@gmail.com";
    }

    public string? GetFullName()
    {
        return "hoangcaophi";
    }

    public string? GetUserIdentity()
    {
        return Guid.CreateVersion7().ToString();
    }

    public bool IsAdminRole()
    {
        return true;
    }
}
