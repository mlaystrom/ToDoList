using Microsoft.AspNetCore.Mvc;
using ToDoList.Models.User;
using ToDoList.Services.User;

namespace ToDoList.WebMvc.Controllers;

public class AccountController : Controller
{
    private IUserService _userService;
    public AccountController(IUserService userService)
    {
        _userService = userService;
    }


//GET action for Register -> return the view to the user
public IActionResult Register()
{
    return View();
}

//POST action for Register

[HttpPost, ValidateAntiForgeryToken]
public async Task<IActionResult> Register(UserRegister model)
{
    if (!ModelState.IsValid)
    {
        return View(model);
    }

    var registerResult = await _userService.RegisterUserAsync(model);
    if (registerResult == false)
    {
        return View(model);
    }
    UserLogin loginModel = new()
    {
        UserName = model.UserName,
        Password = model.Password
    };
    await _userService.LoginUserAsync(loginModel);
    return RedirectToAction("Index", "Home");
}

//GET Login
public IActionResult Login()
{
    return View();
}

//POST Login
[HttpPost, ValidateAntiForgeryToken]
public async Task<IActionResult>Login(UserLogin model)
{
    var loginResult = await _userService.LoginUserAsync(model);
    if (loginResult == false)
    {
        return View(model);
    }
    return RedirectToAction("Index", "Home");
}

public async Task<IActionResult> Logout()
{
    await _userService.LogoutAsync();
    return RedirectToAction("Index", "Home");
}
}