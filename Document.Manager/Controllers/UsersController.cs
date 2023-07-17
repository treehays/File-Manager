using Document.Manager.Models.DTOs.UserDTOs;
using Document.Manager.Services;
using Microsoft.AspNetCore.Mvc;

namespace Document.Manager.Controllers;
public class UsersController : Controller
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> GetUsers()
    {

        return View();
    }

    public async Task<IActionResult> GetUserDocuments(GetUserDocumentsRequestModel model)
    {
        var documents = await _userService.GetByEmailAndTransactionIdAsync(model);
        if (!documents.Status)
        {
            TempData["failed"] = documents.Message;
        }
        TempData["success"] = documents.Message;

        return View(documents);
    }

    public async Task<IActionResult> AddUser()
    {

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(AddUserRequestModelDTO model)
    {
        var user = await _userService.AddAsync(model);
        if (!user.Status)
        {
            TempData["failed"] = user.Message;
        }
        TempData["success"] = user.Message;
        return View();
    }
}
