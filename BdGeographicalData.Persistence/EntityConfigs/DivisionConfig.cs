using BdGeographicalData.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BdGeographicalData.Persistence.EntityConfigs;

public class DivisionConfig : IEntityTypeConfiguration<Division>
{
    public void Configure(EntityTypeBuilder<Division> builder)
    {
        builder.ToTable("Divisions");

        builder.Property(x => x.EnglishName).HasMaxLength(50);
        builder.Property(x => x.BanglaName).HasMaxLength(50);

        builder.HasIndex(x => x.EnglishName).IsUnique(unique: true);
        builder.HasIndex(x => x.BanglaName).IsUnique(unique: true);
    }
}