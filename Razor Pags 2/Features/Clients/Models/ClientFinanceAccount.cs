using System.ComponentModel.DataAnnotations;

namespace Razor_Pags_2.Features.Clients.Models;

public class ClientFinanceAccount
{
    [Key]
    public int Id { get; set; }
    public int ClientId { get; set; }
    public Client? Client { get; set; }
    public int FinanceAccountId { get; set; }
    public FinanceAccount? FinanceAccount { get; set; }
}