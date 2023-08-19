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


    //by default, a GET method
    public async Task<IActionResult> Index()
    {
        List<CategoryListItem> category = await _service.GetAllCategoryAsync();
        return View(category);
    }

    //GET endpoint to get the Create View
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    //Post endpoint to take in the user-submitted data from the form
    //will trun into C# data and stored in the Db
    [HttpPost]

    public async Task<IActionResult> Create(CategoryCreate model)
    {
        if (!ModelState.IsValid)
            return View(model);

        //calling service method to create a new category
        await _service.CreateCategoryAsync(model);
        return RedirectToAction(nameof(Index));
    }


  
}