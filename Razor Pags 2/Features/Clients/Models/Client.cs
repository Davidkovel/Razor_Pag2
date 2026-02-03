using System.ComponentModel.DataAnnotations;

namespace Razor_Pags_2.Features.Clients.Models;

public class Client
{
    [Key] // 
    public int Id { get; set; }
    public string? Surname { get; set; }
    public string? Firtname { get; set; }
    public string? Patronymic { get; set; }
    public string? Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    // One to many
    public ICollection<Phone> Phones { get; set; } = new List<Phone>();
    // One to one
    public Address? Address { get; set; }
}