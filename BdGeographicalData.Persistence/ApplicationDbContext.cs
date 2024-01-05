using BdGeographicalData.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BdGeographicalData.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Division> Divisions { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<SubDistrict> SubDistricts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}