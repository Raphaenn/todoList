using System.ComponentModel.DataAnnotations;

namespace TodoList.Models;

public class UserModel
{
    [Key]
    [Required]
    public int Id { get; set; }

    // public string Name { get; set; } = string.Empty;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}