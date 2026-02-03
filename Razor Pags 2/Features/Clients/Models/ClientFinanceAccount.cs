namespace Razor_Pags_2.Features.Clients.Models;

public class ClientFinanceAccount
{
    public int ClientId { get; set; }
    public Client? Client { get; set; }
    public int FinanceAccountId { get; set; }
    public FinanceAccount? FinanceAccount { get; set; }
}