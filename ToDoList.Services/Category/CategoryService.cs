using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Data.Entities;
using ToDoList.Models.Category;

namespace ToDoList.Services.Category;

public class CategoryService : ICategoryService
{
    private readonly ToDoListDbContext _context;

    private readonly int _userId;
    public CategoryService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager,ToDoListDbContext context)
    {
        var user = signInManager.Context.User; //looking at who is signed in within the current context

        var claim = userManager.GetUserId(user); //looking at current user and getting the Id claim
        int.TryParse(claim, out _userId); //taking that claim and converting from a string to an integer and saving to the field _userId
        _context = context;
    }

    public async Task<List<CategoryListItem>> GetAllCategoryAsync()
    {
        var category = await _context.Category
        .Where(c => c.UserId == _userId)
        .Select(c => new CategoryListItem
        {
            Id= c.Id,
            Type = c.Type
        })
        .ToListAsync();
        return category;
    }

    Task<List<CategoryListItem>> ICategoryService.GetAllCategoryAsync()
    {
        throw new NotImplementedException();
    }
}