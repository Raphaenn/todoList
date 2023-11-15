using System.ComponentModel.DataAnnotations;

namespace TodoList.Models.Entity;

public class TaskModel
{
    [Key]
    public int Id { get; set; }

    // public string Name { get; set; } = null!;
    
    [Required]
    public string? Name { get; set; }
    
    [Required]
    public string? Description { get; set; }
    
    [Required]
    public TaskStatus Status { get; set; }
    
    public int? UserId { get; set; }
    
    public virtual UserModel? User { get; set; }
}