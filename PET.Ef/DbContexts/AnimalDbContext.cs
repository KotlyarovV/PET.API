using Microsoft.EntityFrameworkCore;
using PET.Domain.Models;
using PET.Ef.EntityTypeConfigurations;

namespace PET.Ef.DbContexts
{
    public class AnimalDbContext : DbContext
    {
        public AnimalDbContext(DbContextOptions<AnimalDbContext> options)
            : base(options)
        {

        }

        public DbSet<Animal> Animals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .ApplyConfiguration(new AnimalEntityTypeConfiguration())
                .ApplyConfiguration(new FileEntityTypeConfiguration());
        }
    }
}
    