using Document.Manager.Models.DTOs.FileDTOs;
using Document.Manager.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Document.Manager.Models.DTOs.UserDTOs;

public class UsersDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public long TransactionNumber { get; set; } //= new Random().Next(1000000000, 2147483647);
    public ICollection<FileResponseModel> AttachedDocuments { get; set; } = new HashSet<FileResponseModel>();
}
