using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Data.Entities;
using ToDoList.Models.ToDo;
using ToDoList.Services.ToDo;

namespace ToDoList.Services.ToDo;

public class ToDoService : IToDoService
{
    private readonly ToDoListDbContext _context;

    //allows us to obtain ToDo by current user
    private int _userId;

    //setting up service methods
    public ToDoService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, ToDoListDbContext context)
    {
        var user = signInManager.Context.User; //looking at who is signed in within current context

        var claim = userManager.GetUserId(user); //looking at current user and getting the Id claim

        int.TryParse(claim, out _userId); //taking that claim and convertin from a string to an integer and saving to _userId field

        _context = context;
    }

    public async Task<List<ToDoListItem>> GetAllToDoAsync()
    {
        var activity = await _context.ToDo
        .Where(a => a.UserId == _userId)
        .Select(a => new ToDoListItem
        {
            Id = a.Id,
            UserId = a.UserId,
            CategoryId = a.CategoryId,
            Description = a.Description,
            FinishByDate = a.FinishByDate
        }
        )
        .ToListAsync();
        return activity;
    }
}