using Microsoft.AspNetCore.Identity;
using ToDoList.Data;
using ToDoList.Data.Entities;
using ToDoList.Models.User;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Services.User;

public class UserService : IUserService
{
    private readonly ToDoListDbContext _context;
    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInManager;

    //using this to set up User service methods
    //the methods communicate with the Db and return formatted C# objects that the controller will use
    public UserService(
        ToDoListDbContext context,
        UserManager<UserEntity> userManager,
        SignInManager<UserEntity> signInManager
    )
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<bool> RegisterUserAsync(UserRegister model)
    {
       if (await UserExistsAsync(model.Email, model.UserName))
            return false;

        UserEntity user = new()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.UserName,
            Email = model.Email
        };

        var createResult = await _userManager.CreateAsync(user, model.Password);
            return createResult.Succeeded;

    }

    public async Task<bool> LoginUserAsync(UserLogin model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);
        if(user is null)
            return false;

        var isvalidPassword = await _userManager.CheckPasswordAsync(user, model.Password);
        if (isvalidPassword == false)
            return false;
        
        await _signInManager.SignInAsync(user, true);
            return true;
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    private async Task<bool> UserExistsAsync(string email, string username)
    {
        var normalizedEmail = _userManager.NormalizeEmail(email);
        var normalizedUserName = _userManager.NormalizeName(username);

        return await _context.User.AnyAsync(u =>
        u.NormalizedEmail == normalizedEmail || u.NormalizedUserName == normalizedUserName);
    }
}