using Document.Manager.Models.DTOs.UserDTOs;
using Document.Manager.Models.Entities;
using System.Linq.Expressions;

namespace Document.Manager.Services;

public interface IUserService
{
    Task<UserResponseModelDTO> AddAsync(AddUserRequestModelDTO model);
    Task<UserResponseModelDTO> GetByEmailAndTransactionIdAsync(GetUserDocumentsRequestModel model);
    Task<UsersResponseModelDTO> GetAllUsersAsync();
}
