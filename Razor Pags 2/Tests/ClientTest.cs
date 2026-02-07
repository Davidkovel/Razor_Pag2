using Microsoft.EntityFrameworkCore;
using Razor_Pags_2.Data;
using Razor_Pags_2.Features.Clients.Models;
using Xunit;

namespace Razor_Pags_2.Tests;

public class ClientTest
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public void CreateClient()
    {
        var db = GetDbContext();

        var client = new Client
        {
            Surname = "Ivanov",
            Firtname = "Ivan",
            Patronymic = "Ivanovich",
            Email = "ivan@gmail.com"
        };

        db.Clients.Add(client);
        db.SaveChanges();

        var savedClient = db.Clients.Single();

        Assert.Equal("Ivanov", savedClient.Surname);
        Assert.Equal("Ivan", savedClient.Firtname);
        Assert.Equal("Ivanovich", savedClient.Patronymic);
        Assert.Equal("ivan@gmail.com", savedClient.Email);
    }
    
    [Fact]
    public void GetAllClients_ShouldReturnAllClients()
    {
        var db = GetDbContext();

        db.Clients.AddRange(
            new Client { Surname = "Ivanov", Firtname = "Ivan" },
            new Client { Surname = "Petrov", Firtname = "Petr" },
            new Client { Surname = "Sidorov", Firtname = "Sid" }
        );

        db.SaveChanges();

        var clients = db.Clients.ToList();

        Assert.Equal(3, clients.Count);
        Assert.Contains(clients, c => c.Surname == "Ivanov");
        Assert.Contains(clients, c => c.Surname == "Petrov");
    }
    
    [Fact]
    public void DeleteClient_ShouldRemoveClient()
    {
        var db = GetDbContext();

        var client = new Client { Surname = "DeleteMe" };
        db.Clients.Add(client);
        db.SaveChanges();

        db.Clients.Remove(client);
        db.SaveChanges();

        Assert.Empty(db.Clients);
    }

    
    [Fact]
    public void UpdateClient_ShouldChangeData()
    {
        var db = GetDbContext();

        var client = new Client
        {
            Surname = "OldSurname",
            Email = "old@mail.com"
        };

        db.Clients.Add(client);
        db.SaveChanges();

        client.Surname = "NewSurname";
        client.Email = "new@mail.com";
        db.SaveChanges();

        var updatedClient = db.Clients.Single();

        Assert.Equal("NewSurname", updatedClient.Surname);
        Assert.Equal("new@mail.com", updatedClient.Email);
    }

    [Fact]
    public void ClientPhones_ShouldBeSavedCorrectly()
    {
        var db = GetDbContext();

        var client = new Client
        {
            Surname = "PhoneTest",
            Phones = new List<Phone>
            {
                new Phone { Number = "1234567890" },
                new Phone { Number = "0987654321" }
            }
        };

        db.Clients.Add(client);
        db.SaveChanges();

        var savedClient = db.Clients
            .Include(c => c.Phones)
            .Single();

        Assert.Equal(2, savedClient.Phones.Count);
        Assert.Contains(savedClient.Phones, p => p.Number == "1234567890");
        Assert.Contains(savedClient.Phones, p => p.Number == "0987654321");

        Assert.All(savedClient.Phones, p => 
            Assert.Equal(savedClient.Id, p.ClientId));
    }

    
}