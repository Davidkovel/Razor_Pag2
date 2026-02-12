using System.ComponentModel.DataAnnotations;
using Razor_Pags_2.Features.Clients.Models;

namespace Razor_Pags_2.Features.Clients.ViewModels;

public class ClientViewModel
{
    [Required]
    [MaxLength(50)]
    public string? Surname { get; set; }
    [StringLength(50)]
    public string? Firtname { get; set; }
    [MaxLength(50)]
    public string? Patronymic { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [DataType(DataType.Date)]
    public DateOnly BirthDate { get; set; }
}