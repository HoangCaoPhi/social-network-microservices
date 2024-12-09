using System.Text.Json.Serialization;

namespace EventBus;
public record IntegrationEvent
{
    public IntegrationEvent()
    {
        Id = Guid.CreateVersion7();
        CreationDate = DateTime.UtcNow;
    }

    [JsonInclude]
    public Guid Id { get; set; }

    [JsonInclude]
    public DateTime CreationDate { get; set; }
}
