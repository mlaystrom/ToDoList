using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models.Category;

public class CategoryUpdate
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
}