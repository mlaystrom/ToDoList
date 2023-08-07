using Microsoft.AspNetCore.Identity;
using ToDoList.Data;
using ToDoList.Data.Entities;

namespace ToDoList.Services.User;

public class UserService  //IUserService
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

   // public async Task<bool> RegisterUserAsync(UserRegister model)
   // {
   //     if (await UserExistsAsync(model.Email, model.Username))
   //     return false;

  //      UserEntity user = new()
       // {
  //      FirstName = model.FirstName,
     //   LastName = model.LastName,
   //     UserName = model.UserName,
   //     Email = model.Email
  //      };

  //  }
}