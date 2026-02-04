using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Razor_Pags_2.Features.Clients.Models;

public class Address
{
    public int Id { get; set; }
    [Required]
    [Column(TypeName = "varchar(100)")]
    public string? Country { get; set; }
    [Required]
    [Column(TypeName = "varchar(100)")]
    public string? Region { get; set; }
    [Column(TypeName = "varchar(100)")]
    public string? Area {get; set;}
    [Required]
    [Column(TypeName = "varchar(100)")]
    public string? City { get; set; }
    [Required]
    [Column(TypeName = "varchar(150)")]
    public string? Street { get; set; }
    [Required]
    [Column(TypeName = "varchar(20)")]
    public string? Building {get; set;}
    [Column(TypeName = "varchar(20)")]
    public string? Apartment {get; set;}
    [Column(TypeName = "varchar(10)")]
    public string? Entrance {get; set;}
    [Column(TypeName = "varchar(20)")]
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