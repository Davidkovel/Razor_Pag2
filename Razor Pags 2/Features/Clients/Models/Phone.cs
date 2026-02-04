using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Razor_Pags_2.Features.Clients.Models;

public class Phone
{
    public int Id { get; set; }
    [DataType(DataType.PhoneNumber)]
    [Column(TypeName = "varchar(10)")]
    public string Number { get; set; } = null!;
    public CountryCode CountryCode { get; set; }
    public int ClientId { get; set; }
    public Client? Client { get; set; }
}

public enum CountryCode
{
    UA = 380,
    US = 1, 
    GB = 44, 
    DE = 49,
    FR = 33
}