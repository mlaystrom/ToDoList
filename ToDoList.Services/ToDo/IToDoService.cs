
using ToDoList.Models.ToDo;

namespace ToDoList.Services.ToDo;

public interface IToDoService
{
    Task<List<ToDoListItem>>GetAllToDoAsync();
    Task<bool>CreateToDoItemAsync(CreateListItem model);
    Task<ToDoDetail>GetDetailsByIdAsync(int id);
    Task<bool>UpdateToDoAsync(ToDoUpdate model);
    Task<bool>DeleteToDoAsync(int id);

}