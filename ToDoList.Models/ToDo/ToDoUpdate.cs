using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models.ToDo;

public class ToDoUpdate
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Date Completed")]
    public DateTime FinishByDate { get; set; }
}