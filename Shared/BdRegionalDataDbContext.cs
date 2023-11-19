using BdRegionalData.Api.District.Entity;
using BdRegionalData.Api.Division.Entity;
using BdRegionalData.Api.SubDistrict.Entity;
using Microsoft.EntityFrameworkCore;

namespace BdRegionalData.Shared;

public class BdRegionalDataDbContext : DbContext
{
    public DbSet<Division> Divisions { get; set; } = null!;
    public DbSet<District> Districts { get; set; } = null!;
    public DbSet<SubDistrict> SubDistricts { get; set; } = null!;

    public BdRegionalDataDbContext(DbContextOptions<BdRegionalDataDbContext> options) : base(options)
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