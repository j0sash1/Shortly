using Shortly.Domain.Entities;
using Shortly.Infrastructure.Persistence;
using BCrypt.Net;
namespace Shortly.Infrastructure.Seed;

public class DbInitializer
{
    public static void Initialize(AppDbContext context, ILogger<DbInitializer> log)
    {
        context.Database.EnsureCreated();

        if (context.Users.Any())
        {
            return;
        }

        var users = new User[]{
            new User("john.doe@example.com", BCrypt.Net.BCrypt.HashPassword("password123")),
            new User("jane.smith@example.com", BCrypt.Net.BCrypt.HashPassword("password456"))
        };

        context.Users.AddRange(users);
        context.SaveChanges();
    }
}