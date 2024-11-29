using Shared.Services;

namespace SocialNetwork.ServiceDefaults.Abtractions;
internal class GuidGenerator : IGuidGenerator
{
    public Guid NewGuid()
    {
        return Guid.CreateVersion7();
    }
}
