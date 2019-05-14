using Microsoft.EntityFrameworkCore;
using PET.Domain.Models;

namespace PET.Ef.DbContexts
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}