using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models.User;

public class UserLogin
{
    [Required]
    [Display(Name = "Username")]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}