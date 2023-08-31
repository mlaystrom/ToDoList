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
    public async Task<IActionResult> Create(CreateListItem model)
    {
        if (!ModelState.IsValid)
        return View(model);

        await _service.CreateToDoItemAsync(model);

        return RedirectToAction(nameof(Index));


    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        ToDoDetail model = await _service.GetDetailsByIdAsync(id);

        if (model is null)
        return NotFound();

        //if found, returns the model view

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        ToDoDetail detail = await _service.GetDetailsByIdAsync(id);
        if (detail is null)
            return NotFound();
        
        //if not null, will create a ToDoUpdate Model from the ToDo data object to load into view
        ToDoUpdate model = new()
        {
            Id = detail.Id,
            CategoryId = detail.Id,
            Description = detail.Description,
            FinishByDate = detail.FinishByDate
        };
        return View(model);
    }

    [HttpPost]
    // method takes in the Id of the TodDo AND the ToDoUpdate model because it contains the new data
    public async Task<IActionResult> Update (int id, ToDoUpdate model)
    {
        if (!ModelState.IsValid)
        return View(model);

        if (await _service.UpdateToDoAsync(model))
        return RedirectToAction(nameof(Details), new { id = id});

        ModelState.AddModelError("Save Error", "Unable to update. Please try again.");
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        ToDoDetail todo = await _service.GetDetailsByIdAsync(id);

        if (todo is null)
            return RedirectToAction(nameof(Index));
        
        return View(todo);
    }

    [HttpPost]
    [ActionName(nameof(Delete))] //connecting the method ConfirmDelete with the Delete action

    public async Task<IActionResult> ConfirmDelete(int id)
    {
        await _service.DeleteToDoAsync(id);
        return RedirectToAction(nameof(Index));
    }
}