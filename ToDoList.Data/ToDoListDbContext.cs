using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data.Entities;

namespace ToDoList.Data;

//allows to use ToDoListDbContext to interact with the Db, manage users and roles and perform tasks related to user authentication and authorization in app
public class ToDoListDbContext : IdentityDbContext<UserEntity, IdentityRole<int>, int>
{
    //constructor of the ToDoListDbContext class
    public ToDoListDbContext(DbContextOptions<ToDoListDbContext>options)
        : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //telling EF Core to use table name "User" for UserEntity
        modelBuilder.Entity<UserEntity>().ToTable("User");
    }
}