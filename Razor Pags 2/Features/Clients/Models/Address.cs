namespace Razor_Pags_2.Features.Clients.Models;

public class Address
{
    public int Id { get; set; }
    public string? Country { get; set; }
    public string? Region { get; set; }
    public string? Area {get; set;}
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? Building {get; set;}
    public string? Apartment {get; set;}
    public string? Entrance {get; set;}
    public string? Room {get; set;}
    public string? Postcode {get; set;}
    // One to One
    public int ClientId { get; set; }
    public Client? Client { get; set; }
    
}