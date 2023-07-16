namespace Document.Manager.Models.Entities;

public class Transaction : AuditableEntity
{
    public int TransactionNumber { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public ICollection<AttachedDocument> AttachedDocuments { get; set; } = new HashSet<AttachedDocument>();
}
