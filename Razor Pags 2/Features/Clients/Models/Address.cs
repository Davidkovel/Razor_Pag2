using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Razor_Pags_2.Features.Clients.Models;

public class Address
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string? Country { get; set; }
    [MaxLength(100)]
    public string? Region { get; set; }
    [MaxLength(100)]
    public string? Area {get; set;}
    [MaxLength(100)]
    public string? City { get; set; }
    [MaxLength(150)]
    public string? Street { get; set; }
    [MaxLength(20)]
    public string? Building {get; set;}
    [MaxLength(20)]
    public string? Apartment {get; set;}
    [MaxLength(10)]
    public string? Entrance {get; set;}
    [MaxLength(20)]
    public string? Room {get; set;}
    public string? Postcode {get; set;}
    
    [Column(TypeName = "timestamptz")]
    public DateTime CreatedAt { get; set; }

    [Column(TypeName = "timestamptz")]
    public DateTime UpdatedAt { get; set; }
    
    // One to One
    public int ClientId { get; set; }
    public Client? Client { get; set; }
    
}