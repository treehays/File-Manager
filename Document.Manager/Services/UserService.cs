using Document.Manager.Gateways.FileServices;
using Document.Manager.Models.DTOs.UserDTOs;
using Document.Manager.Models.Entities;
using Document.Manager.Repositories;
using Mapster;

namespace Document.Manager.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IFileService _fileService;
    public UserService(IUserRepository userRepository, IFileService fileService)
    {
        _userRepository = userRepository;
        _fileService = fileService;
    }

    public async Task<UserResponseModelDTO> AddAsync(AddUserRequestModelDTO model)
    {
        var user = model.Adapt<User>();
        user.TransactionNumber = new Random().Next(1000000000, int.MaxValue);
        var fileResponse = await _fileService.ListOfFilesToSystem(model.FormFiles);
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
