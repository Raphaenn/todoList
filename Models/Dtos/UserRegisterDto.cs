using System.ComponentModel.DataAnnotations;

namespace TodoList.Models.Dtos;

public class UserRegisterDto
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Username { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    public string[]? Roles { get; set; }
}