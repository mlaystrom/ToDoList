using Microsoft.AspNetCore.Identity;

namespace ToDoList.Data.Entities;

public class UserEntity : IdentityUser<int>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
   // public string? Password { get; set; }

    //IdentityUser provides inherited property for Username and Email 
}