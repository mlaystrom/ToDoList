using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace ToDoList.Models.ToDo;

public class ToDoListItem
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Date Completed")]
    public DateTime FinishByDate { get; set; }
}