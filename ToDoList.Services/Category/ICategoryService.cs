using ToDoList.Models.Category;

namespace ToDoList.Services.Category;

public interface ICategoryService
{
    Task<List<CategoryListItem>> GetAllCategoryAsync();
}