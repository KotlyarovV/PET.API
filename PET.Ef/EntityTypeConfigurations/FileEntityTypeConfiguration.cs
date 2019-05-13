using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PET.Domain.Models;

namespace PET.Ef.EntityTypeConfigurations
{
    public class FileEntityTypeConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.HasKey(f => f.Id);
        }
    }
}