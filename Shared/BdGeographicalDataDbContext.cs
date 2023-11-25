using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace BdGeographicalData.Shared;
 
public class BdGeographicalDataDbContext : DbContext
{
    public BdGeographicalDataDbContext(DbContextOptions<BdGeographicalDataDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}