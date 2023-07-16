namespace Document.Manager.Models.DTOs.UserDTOs;

public class UsersResponseModelDTO
{
    public string Message { get; set; }
    public bool Status { get; set; }
    public IList<UsersDTO> Datas { get; set; }
}
