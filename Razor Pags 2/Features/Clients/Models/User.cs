using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Razor_Pags_2.Features.Clients.Models;

public class User
{
    public int Id { get; set; }
    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Login { get; set; }
    [Required]
    [Column(TypeName = "varchar(255)")]
    public string Password { get; set; }
    [Required]
    [Column(TypeName = "varchar(100)")]
    public string Email { get; set; }
    
    [Column(TypeName = "timestamptz")]
    public DateTime CreatedAt { get; set; }
    [Column(TypeName = "timestamptz")]
    public DateTime UpdatedAt { get; set; }

    // ManyToOne
    public int StatusId { get; set; }
    public Status Status { get; set; }

    public int RoleId { get; set; }
    public Role Role { get; set; }
}