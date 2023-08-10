using Microsoft.AspNetCore.Mvc;
using ToDoList.Models.Category;
using ToDoList.Services.Category;

namespace ToDoList.WebMvc.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryService _service;
    public CategoryController(ICategoryService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        List<CategoryListItem> category = await _service.GetAllCategoryAsync();
        return View(category);
    }
}