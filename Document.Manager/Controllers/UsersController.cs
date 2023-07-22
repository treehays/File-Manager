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
        //  ViewBag.Number = TempData.Peek("TNumber");
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
            return View(documents);
        }
        TempData["success"] = documents.Message;
        return View(documents);
    }

    //public async Task<IActionResult> GetUserInformation(string email)
    //{

    //    return View();
    //}

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
            return View();
        }
        //TempData["TNumber"] = user.Data.TransactionNumber;
        TempData["success"] = user.Message;
        return RedirectToAction("index", "Home");
    }
}
