using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Integration.Tests;

public static class TestDbContextFactory
{
    public static AppDbContext Create()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var db = new AppDbContext(options);

        db.Database.EnsureCreated();

        return db;
    }
}