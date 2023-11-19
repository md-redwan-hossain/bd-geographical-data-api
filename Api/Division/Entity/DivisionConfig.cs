using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BdRegionalData.Api.Division.Entity;

public class DivisionConfig : IEntityTypeConfiguration<Division>
{
    public void Configure(EntityTypeBuilder<Division> builder)
    {
        builder.HasIndex(x => x.EnglishName).IsUnique(unique: true);
        builder.HasIndex(x => x.BanglaName).IsUnique(unique: true);

        builder
            .HasMany(x => x.Districts)
            .WithOne(x => x.Division)
            .HasForeignKey(x => x.DivisionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Division_Districts");
    }
}