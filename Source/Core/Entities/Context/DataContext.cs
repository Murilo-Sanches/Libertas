using Microsoft.EntityFrameworkCore;
using Libertas.Source.Core.Entities.Models;

namespace Libertas.Source.Core.Entities.Context;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
}
