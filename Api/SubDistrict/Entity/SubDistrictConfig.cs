using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BdRegionalData.Api.SubDistrict.Entity;

public class SubDistrictConfig : IEntityTypeConfiguration<SubDistrict>
{
    public void Configure(EntityTypeBuilder<SubDistrict> builder)
    {
        builder.HasIndex(x => new { x.EnglishName, x.DistrictId })
            .IsUnique(unique: true);
    }
}