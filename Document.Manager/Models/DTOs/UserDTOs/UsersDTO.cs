using Document.Manager.Models.DTOs.FileDTOs;
using Document.Manager.Models.Enums;

namespace Document.Manager.Models.DTOs.UserDTOs;

public class UsersDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string AttachedDocumentName { get; set; }
    public string AttachedDocumentTitle { get; set; }
    public string AttachedDocumentFileType { get; set; }
    public Gender Gender { get; set; }
    public long TransactionNumber { get; set; } //= new Random().Next(1000000000, 2147483647);
    public ICollection<FileResponseModel> AttachedDocuments { get; set; } = new HashSet<FileResponseModel>();
}
