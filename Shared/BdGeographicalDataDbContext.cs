using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace BdGeographicalData.Shared;
 
public class BdGeographicalDataDbContext(DbContextOptions<BdGeographicalDataDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}