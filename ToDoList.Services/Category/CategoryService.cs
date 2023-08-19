using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Data.Entities;
using ToDoList.Models.Category;

namespace ToDoList.Services.Category;

public class CategoryService : ICategoryService
{
    private readonly ToDoListDbContext _context;

    private readonly int _userId;//filtering by user
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
        .Where(c => c.UserId == _userId)//looking for category list of current user
        .Select(c => new CategoryListItem
        {
            Id= c.Id,
            Type = c.Type
        })
        .ToListAsync(); //converting into a c# list
        return category;
    }

   public async Task<bool> CreateCategoryAsync(CategoryCreate model)
   {
    //making a Category from the model passed into the method
    var entity = new CategoryEntity
    {
    Type = model.Type,
    UserId = _userId
    };

    _context.Category.Add(entity); //add category to the DbSet
    var numberOfChanges = await _context.SaveChangesAsync(); //synching the Db with the DbContext, which will add the new Category to SQL Db
    return numberOfChanges == 1;
   }

    public async Task<CategoryDetail> GetCategoryDetailAsync(int id)
    {
      var category = await _context.Category.FindAsync(id);

      if (category is null)
      return new CategoryDetail(); //an empty CategoryDetail object is returned
      CategoryDetail model = new()
      {
        Id = category.Id,
        Type = category.Type
      };
      return model;
    }
   
}