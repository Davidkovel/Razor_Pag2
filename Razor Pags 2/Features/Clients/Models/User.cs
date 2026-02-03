namespace Razor_Pags_2.Features.Clients.Models;

public class User
{
    public int Id { get; set; }

    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // ManyToOne
    public int StatusId { get; set; }
    public Status Status { get; set; }

    public int RoleId { get; set; }
    public Role Role { get; set; }
}
