using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BdGeographicalData.Api.Division.Entity;

public class DivisionConfig : IEntityTypeConfiguration<Division>
{
    public void Configure(EntityTypeBuilder<Division> builder)
    {
        builder.ToTable("Divisions");
        
        builder.HasIndex(x => x.EnglishName).IsUnique(unique: true);
        builder.HasIndex(x => x.BanglaName).IsUnique(unique: true);

        builder
            .HasMany(x => x.Districts)
            .WithOne(x => x.Division)
            .HasForeignKey(x => x.DivisionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Division_Districts");
    }
}