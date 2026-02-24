using System.ComponentModel.DataAnnotations;

namespace Razor_Pags_2.Features.Clients.ViewModels;

public class AddressViewModel
{
    public string? Country { get; set; }
    public string? Region { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? Building { get; set; }
    public string? Apartment { get; set; }
    public string? Entrance { get; set; }
    public string? Room { get; set; }
    public string? Postcode { get; set; }
}
