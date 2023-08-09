using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data.Entities;

namespace ToDoList.Data;

//allows to use ToDoListDbContext to interact with the Db, manage users and roles and perform tasks related to user authentication and authorization in app
public class ToDoListDbContext : IdentityDbContext<UserEntity, IdentityRole<int>, int>
{
    //constructor of the ToDoListDbContext class
    public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options)
        : base(options) { }
    public virtual DbSet<UserEntity> User { get; set; }
    public virtual DbSet<ToDoEntity> ToDo { get; set; }
    public virtual DbSet<CategoryEntity> Category { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //telling EF Core to use table name "User" for UserEntity
        modelBuilder.Entity<UserEntity>().ToTable("User");
        modelBuilder.Entity<ToDoEntity>().ToTable("ToDo");
        modelBuilder.Entity<CategoryEntity>().ToTable("Category");

    }
}