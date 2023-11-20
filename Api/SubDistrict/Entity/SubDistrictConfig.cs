using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BdGeographicalData.Api.SubDistrict.Entity;

public class SubDistrictConfig : IEntityTypeConfiguration<SubDistrict>
{
    public void Configure(EntityTypeBuilder<SubDistrict> builder)
    {
        builder.ToTable("SubDistricts");
        builder.HasIndex(x => new { x.EnglishName, x.DistrictId })
            .IsUnique(unique: true);
    }
}