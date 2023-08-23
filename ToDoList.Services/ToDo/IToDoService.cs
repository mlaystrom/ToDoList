
using ToDoList.Models.ToDo;

namespace ToDoList.Services.ToDo;

public interface IToDoService
{
    Task<List<ToDoListItem>>GetAllToDoAsync();
}