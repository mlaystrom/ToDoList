using ToDoList.Models.Category;

namespace ToDoList.Services.Category;

public interface ICategoryService
{
    Task<List<CategoryListItem>> GetAllCategoryAsync();
    Task<bool>CreateCategoryAsync(CategoryCreate model);

    Task<CategoryDetail> GetCategoryDetailAsync(int id);
}