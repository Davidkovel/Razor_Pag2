using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Razor_Pags_2.Features.Clients.Models;

public class Client
{
    [Key] // 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    [MinLength(1)]
    [Display(Name = "Client Surname")]
    [DataType(DataType.Text)]
    public string? Surname { get; set; }
    [StringLength(50)]
    [DataType(DataType.Text)]
    public string? Firtname { get; set; }
    
    [MaxLength(50)]
    [DataType(DataType.Text)]
    public string? Patronymic { get; set; }
    [MaxLength(100)]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    // One to many
    public ICollection<Phone>? Phones { get; set; } = new List<Phone>();
    // One to one
    public Address? Address { get; set; }
}