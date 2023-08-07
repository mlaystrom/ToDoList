using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Data.Entities;

public class ToDoEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public virtual UserEntity? User { get; set; }

    [Required]
    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }
    public virtual CategoryEntity? Category { get; set; }

    [MaxLength(125)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public DateTime FinishByDate { get; set; }
}