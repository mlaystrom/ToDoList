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

    //GET for the Update method
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        CategoryDetail category = await _service.GetCategoryDetailAsync(id);

        CategoryUpdate model = new()
        {
            Id = category.Id,
            Type = category.Type
        };

        return View(model);
    }

    //POST for Update
    [HttpPost]
    public async Task<IActionResult> Update(int id, CategoryUpdate model)
    {
        if (!ModelState.IsValid)
            return View(model);

        //passing the model to the service method
        //returning the user to the Index view
        if(await _service.UpdateCategoryAsync(model))
            return RedirectToAction("Index", new {id = id});
        
        ModelState.AddModelError("Save Error", "Please Try Again. Unable to update the Category details.");
        return View(model);
    }

    //GET for Delete
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        CategoryDetail category = await _service.GetCategoryDetailAsync(id);
        if (category is null)
        return RedirectToAction(nameof(Index));

        return View(category);
    }

    //POST for Delete
    [HttpPost]
    [ActionName(nameof(Delete))]
    public async Task<IActionResult> ConfirmDelete(int id)
    {
        await _service.DeleteCategoryAsync(id);
        return RedirectToAction(nameof(Index));
    }
}