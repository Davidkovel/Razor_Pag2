using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
        Clients = _context.Clients
            .Include(c => c.Address) 
            .OrderByDescending(c => c.CreatedAt)
            .ToList();
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
            Firtname = Client.Firtname,
            Patronymic = Client.Patronymic,
            Email = Client.Email,
            BirthDate = Client.BirthDate,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,

            Address = new Address
            {
                Country = Client.Address.Country,
                Region = Client.Address.Region,
                City = Client.Address.City,
                Street = Client.Address.Street,
                Building = Client.Address.Building,
                Room = Client.Address.Room,
                Postcode = Client.Address.Postcode,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };
        
        
        _context.Clients.Add(client);
        _context.SaveChanges();
        return RedirectToPage("Index");
    }
}