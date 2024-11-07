using System.ComponentModel.DataAnnotations;

namespace Shared;
public interface IAuditableEntity
{
    public DateTime CreatedAt { get; }

    [MaxLength(50)]
    public string? CreatedBy { get; }

    public DateTime? ModifiedAt { get; }

    [MaxLength(50)]
    public string? ModifiedBy { get; }
}