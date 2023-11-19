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
        modelBuilder.ApplyConfiguration(new DivisionConfig());
        modelBuilder.ApplyConfiguration(new DistrictConfig());
        modelBuilder.ApplyConfiguration(new SubDistrictConfig());

        base.OnModelCreating(modelBuilder);
    }
}