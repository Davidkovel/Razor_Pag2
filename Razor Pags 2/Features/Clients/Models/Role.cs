namespace Razor_Pags_2.Features.Clients.Models;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<User> Users { get; set; }
}