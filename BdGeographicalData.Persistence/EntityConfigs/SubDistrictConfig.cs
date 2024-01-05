using BdGeographicalData.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BdGeographicalData.Persistence.EntityConfigs;

public class SubDistrictConfig : IEntityTypeConfiguration<SubDistrict>
{
    public void Configure(EntityTypeBuilder<SubDistrict> builder)
    {
        builder.ToTable("SubDistricts");

        builder.Property(x => x.EnglishName).HasMaxLength(50);
        builder.Property(x => x.BanglaName).HasMaxLength(50);

        builder.HasIndex(x => new { x.EnglishName, x.DistrictId })
            .IsUnique(unique: true);

        builder
            .HasOne<District>()
            .WithMany()
            .HasForeignKey(x => x.DistrictId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<Division>()
            .WithMany()
            .HasForeignKey(x => x.DivisionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}