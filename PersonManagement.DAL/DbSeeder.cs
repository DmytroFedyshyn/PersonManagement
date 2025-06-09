using Faker;
using Microsoft.EntityFrameworkCore;
using PersonManagement.DAL.Entities;

namespace PersonManagement.DAL;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Persons.AnyAsync()) return;

        var people = Enumerable.Range(1, 10).Select(_ =>
        {
            var address = new Entities.Address
            {
                City = Faker.Address.City(),
                AddressLine = Faker.Address.StreetAddress()
            };

            return new Person
            {
                FirstName = Name.First(),
                LastName = Name.Last(),
                Address = address
            };
        }).ToList();

        await context.Persons.AddRangeAsync(people);
        await context.SaveChangesAsync();
    }
}
