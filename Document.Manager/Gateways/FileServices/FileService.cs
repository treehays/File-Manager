namespace Document.Manager.Gateways.FileServices;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }
    public async Task<FileResponseModel> UploadFileToSystem(IFormFile formFile)
    {
        var fileDestinationPath = Path.Combine(_webHostEnvironment.WebRootPath, "Documents");

        if (formFile is null || formFile.Length is 0)
        {
            var response = new FileResponseModel
            {
                Status = false,
                Message = "file not found",
            };
            return response;
        }

        if (!Directory.Exists(fileDestinationPath)) Directory.CreateDirectory(fileDestinationPath);

        //var fileName = Guid.NewGuid().ToString().Replace('-', 's') + Path.GetExtension(file.FileName);
        var fileName = $"{Ulid.NewUlid()}{formFile.FileName}";
        //var fileNameWithExtension = formFile.FileName;
        var fileWithoutName = Path.GetFileNameWithoutExtension(formFile.FileName);
        var fileType = formFile.ContentType.ToLower();
        var fileExtension = Path.GetExtension(formFile.FileName);
        var fileSizeInKb = formFile.Length / 1024;
        var fileSourcePath = Path.GetFileName(formFile.FileName);
        var destinationFullPath = Path.Combine(fileDestinationPath, fileName);

        using (var stream = new FileStream(destinationFullPath, FileMode.Create))
        {
            await formFile.CopyToAsync(stream);
        }

        var fileSystemReponse = new FileResponseModel
        {
            Status = true,
            Message = "file successfully uploaded",
            Data = new FileDTO
            {
                Extension = fileExtension,
                FileType = fileType,
                Name = fileName,
                Title = fileWithoutName,
                Filesize = fileSizeInKb,
            },
        };
        return fileSystemReponse;
    }

    public async Task<FileDatabaseResponseModel> UploadFileToDatabase(IFormFile formFile)
    {
        var fileDestinationPath = Path.Combine(_webHostEnvironment.WebRootPath, "Documents");

        if (formFile is null || formFile.Length is 0)
        {
            var response = new FileDatabaseResponseModel
            {
                Status = false,
                Message = "file not found",
            };
            return response;
        }

        if (!Directory.Exists(fileDestinationPath)) Directory.CreateDirectory(fileDestinationPath);

        var fileWithoutExtension = Path.GetFileNameWithoutExtension(formFile.FileName);
        var fileName = $"{Ulid.NewUlid()}{formFile.FileName}";
        var fileType = formFile.ContentType.ToLower();
        var fileExtension = Path.GetExtension(formFile.FileName);
        var fileSizeInKb = formFile.Length / 1024;

        var fileReponse = new FileDatabaseResponseModel
        {
            Status = true,
            Message = "file successfully uploaded",
            Data = new FileDatabaseDTO
            {
                Extension = fileExtension,
                FileType = fileType,
                Name = fileWithoutName,
                Filesize = fileSizeInKb,
            },
        };
        //converting
        using (var dataStream = new MemoryStream())
        {
            await formFile.CopyToAsync(dataStream);
            fileReponse.Data.FileStream = dataStream.ToArray();
        }
        return fileReponse;
    }
    public async Task<FilesDatabaseResponseModel> ListOfFilesToDatabase(IList<IFormFile> formFiles, int transactionNumber)
    {
        var fileInfos = new List<FileDatabaseDTO>();

        foreach (var item in formFiles)
        {
            var fileinfo = await UploadFileToDatabase(item);
            fileinfo.Data.TransactionNumber = transactionNumber;
            fileInfos.Add(fileinfo.Data);
        }
        return new FilesDatabaseResponseModel
        {
            Status = true,
            Message = "File successfully Saved",
            Datas = fileInfos,
        };
    }
    public async Task<FilesResponseModel> ListOfFilesToSystem(IList<IFormFile> formFiles, int transactionNumber)
    {
        var fileInfos = new List<FileDTO>();
        foreach (var item in formFiles)
        {
            var fileinfo = await UploadFileToSystem(item);
            fileinfo.Data.TransactionNumber = transactionNumber;
            fileInfos.Add(fileinfo.Data);
        }
        return new FilesResponseModel
        {
            Status = true,
            Message = "File successfully Saved",
            Datas = fileInfos,
        };
    }
}
