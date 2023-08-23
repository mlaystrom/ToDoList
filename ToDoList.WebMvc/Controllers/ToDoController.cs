using Microsoft.AspNetCore.Mvc;
using ToDoList.Models.ToDo;
using ToDoList.Services.ToDo;

namespace ToDoList.WebMvc.Controllers;

public class ToDoController : Controller
{
    private readonly IToDoService _service;

    public ToDoController(IToDoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<ToDoListItem> activity = await _service.GetAllToDoAsync();
        return View(activity);
    }   
}