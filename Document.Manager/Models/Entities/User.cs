using System.ComponentModel.DataAnnotations;

namespace Document.Manager.Models.Entities;

public class User : AuditableEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Key]
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public long TransactionNumber { get; set; } //= new Random().Next(1000000000, 2147483647);
    public ICollection<AttachedDocument> AttachedDocuments { get; set; } = new HashSet<AttachedDocument>();

}
