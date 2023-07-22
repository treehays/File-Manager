namespace Document.Manager.Models.DTOs.UserDTOs;

public class UserInformationResponseModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool Status { get; set; }
    public string Message { get; set; }
    public DateTime DateOfBirth { get; set; }
}

