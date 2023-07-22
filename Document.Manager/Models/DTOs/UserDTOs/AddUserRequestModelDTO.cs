using System.ComponentModel.DataAnnotations;
using Document.Manager.Models.Enums;

namespace Document.Manager.Models.DTOs.UserDTOs;

public class AddUserRequestModelDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }

    //public string PhoneNumber { get; set; }
    //public string Address { get; set; }
    public Gender Gender { get; set; }
    public IList<IFormFile> FormFiles { get; set; }
    public IList<string> Idms { get; set; }
}
