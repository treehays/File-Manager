namespace Document.Manager.Models.Entities;

public class AttachedDocument : AuditableEntity
{
    public string Name { get; set; }
    public string? Title { get; set; }
    public byte[]? FileStream { get; set; }
    public string UserEmail { get; set; }
    //[ForeignKey("Email")]
    public User User { get; set; }
}
