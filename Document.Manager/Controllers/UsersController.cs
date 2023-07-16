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
    public async Task<IActionResult> GetUserDocuments()
    {

        return View();
    }

    public async Task<IActionResult> AddUser()
    {

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(AddUserRequestModelDTO model)
    {
        var user = await _userService.AddAsync(model);
        return View();
    }
}
