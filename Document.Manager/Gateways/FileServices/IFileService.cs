using Document.Manager.Models.DTOs.FileDTOs;

namespace Document.Manager.Gateways.FileServices;

public interface IFileService
{
    Task<FileResponseModel> UploadFileToSystem(IFormFile file);
    Task<FileDatabaseResponseModel> UploadFileToDatabase(IFormFile file);
    Task<FilesDatabaseResponseModel> ListOfFilesToDatabase(IList<IFormFile> formFiles);
    Task<FilesResponseModel> ListOfFilesToSystem(IList<IFormFile> formFiles);
}
