﻿using Document.Manager.Models.DTOs.UserDTOs;

namespace Document.Manager.Services;

public interface IUserService
{
    Task<UserResponseModelDTO> AddAsync(AddUserRequestModelDTO model);
    Task<UserInformationResponseModel> GetUserInfomationAsync(string email);
    Task<UsersResponseModelDTO> GetByEmailAndTransactionIdAsync(GetUserDocumentsRequestModel model);
    //Task<UsersResponseModelDTO> GetAllUsersAsync();
}
