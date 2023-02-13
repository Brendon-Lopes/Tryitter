using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEndTryitter.Models;

public class User
{
    [Key]
    public Guid UserId { get; set; }
    public string FullName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    [Range(1, 4)]
    public int CurrentModule { get; set; }
    public string? StatusMessage { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    [InverseProperty("User")]
    public ICollection<Post>? Posts { get; set; }
}