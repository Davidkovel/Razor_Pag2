using Microsoft.EntityFrameworkCore;
using Razor_Pags_2.Data;

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

    
}