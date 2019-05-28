using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PET.Domain.Models;

namespace PET.Ef.EntityTypeConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(a => a.Animals)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}