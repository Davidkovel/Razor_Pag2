using System.ComponentModel.DataAnnotations;
using Razor_Pags_2.Features.Clients.Models;

namespace Razor_Pags_2.Features.Clients.ViewModels;

public class PhoneViewModel
{
    [Required]
    [StringLength(50)]
    public string? Phone { get; set; }
    [Required]
    public CountryCode ? CountryCode { get; set; }
}