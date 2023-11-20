using System.Reflection;
using System.Reflection.Metadata;
using BdGeographicalData.Api.District.Entity;
using BdGeographicalData.Api.Division.Entity;
using BdGeographicalData.Api.SubDistrict.Entity;
using Microsoft.EntityFrameworkCore;

namespace BdGeographicalData.Shared;

public class BdGeographicalDataDbContext : DbContext
{
    public DbSet<Division> Divisions { get; set; } = null!;
    public DbSet<District> Districts { get; set; } = null!;
    public DbSet<SubDistrict> SubDistricts { get; set; } = null!;

    public BdGeographicalDataDbContext(DbContextOptions<BdGeographicalDataDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}