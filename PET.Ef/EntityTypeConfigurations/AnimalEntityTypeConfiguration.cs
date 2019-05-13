using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PET.Domain;
using PET.Domain.Models;

namespace PET.Ef.EntityTypeConfigurations
{
    public class AnimalEntityTypeConfiguration : IEntityTypeConfiguration<Animal>
    {
        public void Configure(EntityTypeBuilder<Animal> builder)
        {
            builder.HasMany(a => a.Files)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
