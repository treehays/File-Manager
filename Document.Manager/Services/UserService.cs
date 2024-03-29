﻿using Document.Manager.Gateways.EmailServices;
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
        var userExistByEmail = await _userRepository.GetAsync(x => x.Email == model.Email);
        if (userExistByEmail != null)
        {
            return new UserResponseModelDTO
            {
                Status = false,
                Message = "User already exist",
            };
        }
        var user = model.Adapt<User>();
        var transactionNumber = new Random().Next(1000000000, int.MaxValue);

        var fileResponse = await _fileService.ListOfFilesToSystem(model.FormFiles, transactionNumber);
        //listEmail
        var listOfAttachment = new List<SendSmtpEmailAttachment>();
        foreach (var item in fileResponse.Datas)
        {
            var attachmentContent = new SendSmtpEmailAttachment
            {
                Name = item.Name,
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
        var attachedDocuments = fileResponse.Datas.Adapt<List<AttachedDocument>>();

        var transaction = model.Adapt<Transaction>();
        transaction.TransactionNumber = transactionNumber;
        transaction.AttachedDocuments = attachedDocuments;
        user.Transactions.Add(transaction);

        await _userRepository.AddAsync(user);
        var response = new UserResponseModelDTO
        {
            Status = true,
            Message = $"Succesfully Created.. and your transaction Id is {transactionNumber}",
            Data = user.Adapt<UsersDTO>(),
        };
        response.Data.TransactionNumber = transactionNumber;
        return response;
    }



    public async Task<UsersResponseModelDTO> GetByEmailAndTransactionIdAsync(GetUserDocumentsRequestModel model)
    {
        if (model is null)
        {
            return new UsersResponseModelDTO
            {
                Status = false,
                Message = "Enter valid word",
                Datas = Enumerable.Empty<UsersDTO>().ToList(),
            };
        }

        var user = await _userRepository.GetDocumentsByEmailAndTransactionNumberAsync(model.Email, model.TransactionNumber);

        if (user.Count == 0)
        {
            return new UsersResponseModelDTO
            {
                Status = false,
                Message = "No document yet",
                Datas = Enumerable.Empty<UsersDTO>().ToList(),
            };
        }
        var userInfo = await _userRepository.GetAsync(x => x.Email == model.Email);
        var userModel = userInfo.Adapt<UserInformationResponseModel>();
        var response = new UsersResponseModelDTO
        {
            Status = true,
            Message = "Retrirved succesfully", 
            UserInformationResponseModel = userModel,
            Datas = user.Select(x =>
            new UsersDTO
            {
                AttachedDocumentFileType = x.FileType,
                AttachedDocumentName = x.Name,
                AttachedDocumentTitle = x.Title,
                TransactionNumber = model.TransactionNumber,
                //Email = x.Transaction.User.Email,
            }).ToList(),
            //Datas = user.Adapt<List<UsersDTO>>(),
        };
        return response;
    }

    public async Task<UserInformationResponseModel> GetUserInfomationAsync(string email)
    {
        var user = await _userRepository.GetAsync(x => x.Email == email);
        if (user is null)
        {
            return new UserInformationResponseModel
            {
                Status = false,
                Message= "user not found."
            };
        }
        var userModel = user.Adapt<UserInformationResponseModel>();
        userModel.Status = true;
        userModel.Message = "User retrived sucess";
        return userModel;
    }
}
