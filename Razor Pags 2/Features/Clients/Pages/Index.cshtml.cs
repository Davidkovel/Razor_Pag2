using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor_Pags_2.Data;
using Razor_Pags_2.Features.Clients.Models;
using Razor_Pags_2.Features.Clients.ViewModels;

namespace Razor_Pags_2.Features.Clients.Pages;

public class Index : PageModel
{
    private readonly AppDbContext _context;

    public Index(AppDbContext context)
    {
        _context = context;
    }
    
    [BindProperty]
    public ClientViewModel Client { get; set; } = new();
    public List<Client> Clients { get; set; } = new();
    public void OnGet()
    {
        Clients = _context.Clients.OrderByDescending(c => c.CreatedAt).ToList();
    }

    public IActionResult OnPost()
    {
        Console.WriteLine("OnPost");
        Console.WriteLine(Client.Surname);
        if (!ModelState.IsValid)
        {
            Clients = _context.Clients.OrderByDescending(c => c.CreatedAt).ToList();    
            return Page();
        }

        var client = new Client
        {
            Surname = Client.Surname,
            Patronymic = Client.Patronymic,
            Email = Client.Email,
            BirthDate = Client.BirthDate,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        _context.Clients.Add(client);
        _context.SaveChanges();
        return RedirectToPage("Index");
    }
}