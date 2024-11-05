using Microsoft.EntityFrameworkCore;
using WebAPITest1.Models.Entities;

namespace WebAPITest1.Data;

public interface IApplicationDbContext
{
    DbSet<Employee> Employees { get; set; }
}

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }
}

