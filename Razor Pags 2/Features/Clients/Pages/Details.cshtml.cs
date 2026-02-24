using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Razor_Pags_2.Data;
using Razor_Pags_2.Features.Clients.Models;
using Razor_Pags_2.Features.Clients.ViewModels;

namespace Razor_Pags_2.Pages;

public class Details : PageModel
{
    private readonly AppDbContext _context;

    public Details(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Client Client { get; set; } = null!;

    [BindProperty]
    public Address Address { get; set; } = new();

    [BindProperty]
    public Phone NewPhone { get; set; } = new();

    public List<Phone> Phones { get; set; } = new();

    public IActionResult OnGet(int id)
    {
        Client = _context.Clients
            .Include(c => c.Address)
            .Include(c => c.Phones)
            .FirstOrDefault(c => c.Id == id);
// S * ... FROM Cli.. AS C INNER JOIN LL AS L ON C.Id == L.AS
        if (Client == null) return NotFound();

        Address = Client.Address ?? new Address();

        Phones = Client.Phones.ToList();

        return Page();
    }

    public IActionResult OnPostSave(int id)
    {
        var client = _context.Clients
            .Include(c => c.Address)
            .FirstOrDefault(c => c.Id == id);

        if (client == null) return NotFound();

        client.Surname = Client.Surname;
        client.Firtname = Client.Firtname;
        client.Patronymic = Client.Patronymic;
        client.Email = Client.Email;
        client.BirthDate = Client.BirthDate;
        client.UpdatedAt = DateTime.UtcNow;

        if (client.Address == null)
        {
            client.Address = Address;
        }
        else
        {
            client.Address.Country = Address.Country;
            client.Address.Region = Address.Region;
            client.Address.City = Address.City;
            client.Address.Street = Address.Street;
            client.Address.Building = Address.Building;
            client.Address.Apartment = Address.Apartment;
            client.Address.Entrance = Address.Entrance;
            client.Address.Room = Address.Room;
            client.Address.Postcode = Address.Postcode;
            client.Address.UpdatedAt = DateTime.UtcNow;
        }

        _context.SaveChanges();

        return RedirectToPage(new { id });
    }

    public IActionResult OnPostAddPhone(int id)
    {
        NewPhone.ClientId = id;
        _context.Phones.Add(NewPhone);
        _context.SaveChanges();

        return RedirectToPage(new { id });
    }

    public IActionResult OnPostDeletePhone(int phoneId, int id)
    {
        var phone = _context.Phones.FirstOrDefault(p => p.Id == phoneId);
        if (phone != null)
        {
            _context.Phones.Remove(phone);
            _context.SaveChanges();
        }

        return RedirectToPage(new { id });
    }

    public IActionResult OnPostDeleteClient(int id)
    {
        var client = _context.Clients
            .Include(c => c.Phones)
            .Include(c => c.Address)
            .FirstOrDefault(c => c.Id == id);

        if (client == null) return NotFound();

        _context.Clients.Remove(client);
        _context.SaveChanges();

        return RedirectToPage("Index");
    }
}