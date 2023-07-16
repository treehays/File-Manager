namespace Document.Manager.Models.Entities;

public abstract class AuditableEntity : BaseEntity
{
    public string? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public string? LastModifiedBy { get; set; }
    public DateTime LastModifiedOn { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; set; }
}
