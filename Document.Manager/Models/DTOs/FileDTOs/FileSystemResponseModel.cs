namespace Document.Manager.Models.DTOs.FileDTOs;

public class FileDatabaseResponseModel
{
    public string Message { get; set; }

    public bool Status { get; set; }
    public FileDatabaseDTO Data { get; set; }
}
public class FilesDatabaseResponseModel
{
    public string Message { get; set; }

    public bool Status { get; set; }
    public IList<FileDatabaseDTO> Datas { get; set; }
}
public class FileDatabaseDTO
{
    public string Name { get; set; }
    public string FileType { get; set; }
    public string Extension { get; set; }
    public long Filesize { get; set; }
    public string Title { get; set; }
    public byte[] FileStream { get; set; }
}
