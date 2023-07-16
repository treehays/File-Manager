namespace Document.Manager.Models.DTOs.FileDTOs;

public class FileResponseModel
{
    public string Message { get; set; }

    public bool Status { get; set; }
    public FileDTO Data { get; set; }
}
public class FilesResponseModel
{
    public string Message { get; set; }

    public bool Status { get; set; }
    public IList<FileDTO> Datas { get; set; }
}
public class FileDTO
{
    public string Name { get; set; }
}
