using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDoList.Models.ToDo;
using ToDoList.Services.Category;
using ToDoList.Services.ToDo;

namespace ToDoList.WebMvc.Controllers;

public class ToDoController : Controller
{
    private IToDoService _service;
    private ICategoryService _categoryService;

    public ToDoController(IToDoService service, ICategoryService categoryService)
    {
        _service = service;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<ToDoListItem> activity = await _service.GetAllToDoAsync();
        return View(activity);
    }   

    [HttpGet]

    public async Task<IActionResult> Create()
    {
        //getting all category types from _CategoryService
        var categories = await _categoryService.GetAllCategoryAsync();
        //converting into a list of selection items 
        var selectList = categories.Select(c => new SelectListItem(c.Type, c.Id.ToString())).ToList();
        //The selectList is stored in the ViewData dictionary with the key "Categories"
        //This allows the data to be passed to the view and accessed within the view's HTML code
        ViewData["Categories"] = selectList;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult>Create(CreateListItem model)
    {
        if (!ModelState.IsValid)
        return View(model);

        await _service.CreateToDoItemAsync(model);

        return RedirectToAction(nameof(Index));


    }
}