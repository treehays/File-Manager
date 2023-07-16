namespace Document.Manager.Models.DTOs.UserDTOs;

public class UserResponseModelDTO
{
    public string Message { get; set; }

    public bool Status { get; set; }
    public UsersDTO Data { get; set; }
}
