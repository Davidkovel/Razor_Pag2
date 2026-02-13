using Microsoft.EntityFrameworkCore;
using Razor_Pags_2.Data;
using Razor_Pags_2.Features.Clients.Models;
using Xunit;

namespace Razor_Pags_2.Tests;

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class ModelTests
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
        using var db = GetDbContext();

        var client = new Client
        {
            Surname = "Ivanov",
            Firtname = "Ivan",
            Patronymic = "Ivanovich",
            Email = "ivan@gmail.com",
            BirthDate = new DateOnly(1990, 1, 1),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        db.Clients.Add(client);
        db.SaveChanges();

        var saved = db.Clients.Single();

        Assert.Equal("Ivanov", saved.Surname);
        Assert.Equal("Ivan", saved.Firtname);
    }

    [Fact]
    public void CreateClientWithAddress()
    {
        using var db = GetDbContext();

        var client = new Client
        {
            Surname = "Petrov",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var address = new Address
        {
            Country = "Ukraine",
            Region = "Kyiv",
            City = "Kyiv",
            Street = "Main",
            Building = "10",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Client = client
        };

        db.Add(address);
        db.SaveChanges();

        var saved = db.Addresses.Include(a => a.Client).Single();

        Assert.Equal("Ukraine", saved.Country);
        Assert.Equal("Petrov", saved.Client.Surname);
    }

    [Fact]
    public void AddPhoneToClient()
    {
        using var db = GetDbContext();

        var client = new Client
        {
            Surname = "Test",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var phone = new Phone
        {
            Number = "1234567890",
            CountryCode = CountryCode.UA,
            Client = client
        };

        db.Phones.Add(phone);
        db.SaveChanges();

        var saved = db.Phones.Include(p => p.Client).Single();

        Assert.Equal(CountryCode.UA, saved.CountryCode);
        Assert.Equal("Test", saved.Client.Surname);
    }

    [Fact]
    public void CreateFinanceAccount()
    {
        using var db = GetDbContext();

        var account = new FinanceAccount
        {
            Balance = 1000m,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        db.FinanceAccounts.Add(account);
        db.SaveChanges();

        var saved = db.FinanceAccounts.Single();

        Assert.Equal(1000m, saved.Balance);
    }

    [Fact]
    public void LinkClientToFinanceAccount()
    {
        using var db = GetDbContext();

        var client = new Client
        {
            Surname = "Bond",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var account = new FinanceAccount
        {
            Balance = 5000m,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        db.Add(client);
        db.Add(account);
        db.SaveChanges();

        var link = new ClientFinanceAccount
        {
            ClientId = client.Id,
            FinanceAccountId = account.Id
        };

        db.ClientFinanceAccounts.Add(link);
        db.SaveChanges();

        var saved = db.ClientFinanceAccounts.Single();

        Assert.Equal(client.Id, saved.ClientId);
        Assert.Equal(account.Id, saved.FinanceAccountId);
    }

    [Fact]
    public void CreateRole()
    {
        using var db = GetDbContext();

        var role = new Role
        {
            Name = "Admin"
        };

        db.Roles.Add(role);
        db.SaveChanges();

        Assert.Equal("Admin", db.Roles.Single().Name);
    }

    [Fact]
    public void CreateStatus()
    {
        using var db = GetDbContext();

        var status = new Status
        {
            Name = "Active"
        };

        db.Statuses.Add(status);
        db.SaveChanges();

        Assert.Equal("Active", db.Statuses.Single().Name);
    }

    [Fact]
    public void CreateUserWithRoleAndStatus()
    {
        using var db = GetDbContext();

        var role = new Role { Name = "User" };
        var status = new Status { Name = "Active" };

        db.Add(role);
        db.Add(status);
        db.SaveChanges();

        var user = new User
        {
            Login = "testuser",
            Password = "123456",
            Email = "test@mail.com",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            RoleId = role.Id,
            StatusId = status.Id
        };

        db.Users.Add(user);
        db.SaveChanges();

        var saved = db.Users
            .Include(u => u.Role)
            .Include(u => u.Status)
            .Single();

        Assert.Equal("User", saved.Role.Name);
        Assert.Equal("Active", saved.Status.Name);
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