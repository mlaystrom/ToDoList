using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Data.Entities;
using ToDoList.Models.ToDo;
using ToDoList.Services.ToDo;

namespace ToDoList.Services.ToDo;

public class ToDoService : IToDoService
{
    private ToDoListDbContext _context;

    //allows us to obtain ToDo by current user
    private int _userId;
    private int _categoryId;

    //setting up service methods
    public ToDoService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, ToDoListDbContext context)
    {
        var user = signInManager.Context.User; //looking at who is signed in within current context

        var claim = userManager.GetUserId(user); //looking at current user and getting the Id claim

        int.TryParse(claim, out _userId); //taking that claim and convertin from a string to an integer and saving to _userId field

        _context = context;
    }

    public async Task<bool> CreateToDoItemAsync(CreateListItem model)
    {
        var entity = new ToDoEntity
        {
            UserId = _userId,
            CategoryId = model.CategoryId,
            Description = model.Description,
            FinishByDate = model.FinishByDate
        };
        //now adding new entity to the ToDoList table
        _context.ToDo.Add(entity);
        var numberOfChanges = await _context.SaveChangesAsync();

        return numberOfChanges == 1;
    }

    public async Task<List<ToDoListItem>> GetAllToDoAsync()
    {
        //getting all todo items from ToDo Entity Db set
        //including todo that are associated with User and Category
        var activity = await _context.ToDo.Include(a => a.User).Include(a => a.Category)
        .Where(a => a.UserId == _userId)
        .Select(a => new ToDoListItem
        {
            Id = a.Id,
            UserId = a.UserId,
            CategoryId = a.CategoryId,
            Category = a.Category.Type, //accessing the type property on the a.Category object
            Description = a.Description,
            FinishByDate = a.FinishByDate
        }
        )
        .ToListAsync();// fetching the data from the Db and returns the results as a list (c#) of ToDoListItem objects
        return activity; //the list of ToDoListItem objects is returned from the method
    }
}