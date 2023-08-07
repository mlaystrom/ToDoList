using ToDoList.Models.User;

namespace ToDoList.Services.User;

public interface IUserService
{
    Task<bool> RegisterUserAsync(UserRegister model);
    Task<bool> LoginUserAsync(UserLogin model);
    Task LogoutAsync();
}