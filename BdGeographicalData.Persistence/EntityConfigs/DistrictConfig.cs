using BdGeographicalData.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BdGeographicalData.Persistence.EntityConfigs;

public class DistrictConfig : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder.ToTable("Districts");

        builder.Property(x => x.EnglishName).HasMaxLength(50);
        builder.Property(x => x.BanglaName).HasMaxLength(50);

        builder.HasIndex(x => new { x.EnglishName, x.DivisionId })
            .IsUnique(unique: true);

        builder
            .HasOne<Division>()
            .WithMany()
            .HasForeignKey(x => x.DivisionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}