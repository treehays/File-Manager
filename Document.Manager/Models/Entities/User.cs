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
    public ICollection<Transaction> Transactions{ get; set; } = new HashSet<Transaction>();

}
