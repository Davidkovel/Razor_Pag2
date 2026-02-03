namespace Razor_Pags_2.Features.Clients.Models;

public class FinanceAccount
{
    public int Id { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}