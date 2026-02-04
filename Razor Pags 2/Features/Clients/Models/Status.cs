using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Razor_Pags_2.Features.Clients.Models;

public class Status
{
    public int Id { get; set; }
    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Name { get; set; }

    public ICollection<User> Users { get; set; }
}