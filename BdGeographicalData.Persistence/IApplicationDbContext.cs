using BdGeographicalData.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BdGeographicalData.Persistence
{
    public interface IApplicationDbContext
    {
        public DbSet<Division> Divisions { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<SubDistrict> SubDistricts { get; set; }
    }
}