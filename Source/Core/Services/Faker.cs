using Libertas.Source.Core.Entities.Context;
using Libertas.Source.Core.Entities.Models;

namespace Libertas.Source.Core.Services
{
    public class Faker(DataContext context)
    {
        private readonly DataContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public void SeedDummyData(int quantity)
        {
            _context.Database.EnsureCreated();

            if (!_context.Users.Any())
            {
                var dummyUsers = SeedUsers(quantity);
                _context.Set<User>().AddRange(dummyUsers);
                _context.SaveChanges();
                Console.WriteLine("Dummy users created.");
            };
        }

        private static IEnumerable<User> SeedUsers(int quantity)
        {
            var faker = new Bogus.Faker();

            for (int i = 0; i < quantity; i++)
            {
                yield return new User
                {
                    FirstName = faker.Name.FirstName(),
                    LastName = faker.Name.LastName(),
                    Username = faker.Internet.UserName(),
                    Email = faker.Internet.Email(),
                    Password = faker.Internet.Password(),
                };
            }
        }
    }
}
