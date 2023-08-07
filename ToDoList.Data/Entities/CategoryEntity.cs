using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Data.Entities;

public class CategoryEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public virtual UserEntity? User { get; set; }

    [Required]
    public string Type { get; set; } = string.Empty;
}