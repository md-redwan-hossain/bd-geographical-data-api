using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BdGeographicalData.Api.District.Entity;

public class DistrictConfig : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder.ToTable("Districts");
        
        builder.HasIndex(x => new { x.EnglishName, x.DivisionId })
            .IsUnique(unique: true);

        builder
            .HasMany(x => x.SubDistricts)
            .WithOne(x => x.District)
            .HasForeignKey(x => x.DistrictId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_District_SubDistricts");
    }
}