using Document.Manager.Gateways.EmailServices;
using Document.Manager.Gateways.FileServices;
using Document.Manager.Models.DTOs.EmailDTOs;
using Document.Manager.Models.DTOs.UserDTOs;
using Document.Manager.Models.Entities;
using Document.Manager.Repositories;
using Mapster;
using sib_api_v3_sdk.Model;

namespace Document.Manager.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IFileService _fileService;
    private readonly IBrevoEmail _brevoEmail;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public UserService(IUserRepository userRepository, IFileService fileService, IBrevoEmail brevoEmail, IWebHostEnvironment webHostEnvironment)
    {
        _userRepository = userRepository;
        _fileService = fileService;
        _brevoEmail = brevoEmail;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<UserResponseModelDTO> AddAsync(AddUserRequestModelDTO model)
    {
        var user = model.Adapt<User>();
        user.TransactionNumber = new Random().Next(1000000000, int.MaxValue);
        var fileResponse = await _fileService.ListOfFilesToSystem(model.FormFiles);
        var listOfAttachment = new List<SendSmtpEmailAttachment>();
        foreach (var item in fileResponse.Datas)
        {
            var attachmentContent = new SendSmtpEmailAttachment
            {
                Name = item.Name,
                Url = "Url.com",
                Content = File.ReadAllBytes(Path.Combine(_webHostEnvironment.WebRootPath, "Documents", item.Name)),
            };
            listOfAttachment.Add(attachmentContent);
        }
        var emailContent = new SendAtDTO
        {
            ReceiverEmail = model.Email,
            Body = "Body",
            Subject = "The Ne Documents",
            ReceiverName = model.FirstName,
            EmailAttachment = listOfAttachment,
        };
        var sendToEmail = await _brevoEmail.SendEmailWithAttachment(emailContent);

        user.AttachedDocuments = fileResponse.Datas.Adapt<List<AttachedDocument>>();
        await _userRepository.AddAsync(user);
        var response = new UserResponseModelDTO
        {
            Status = true,
            Message = "Succesfully Created..",
            Data = user.Adapt<UsersDTO>(),
        };
        return response;
    }

    public async Task<UsersResponseModelDTO> GetAllUsersAsync()
    {
        var users = await _userRepository.GetListAsync(x => !x.IsDeleted);
        var response = new UsersResponseModelDTO
        {
            Status = true,
            Message = "Retrirved succesfully",
            Datas = users.Adapt<List<UsersDTO>>(),
        };
        return response;
    }

    public async Task<UserResponseModelDTO> GetByEmailAsync(string email)
    {
        //var user  = await _userRepository.GetAsync(x => x.Equals(email));
        var user = await _userRepository.GetAsync(x => x.Email == email);
        var response = new UserResponseModelDTO
        {
            Status = true,
            Message = "Retrirved succesfully",
            Data = user.Adapt<UsersDTO>(),
        };
        return response;
    }
}
