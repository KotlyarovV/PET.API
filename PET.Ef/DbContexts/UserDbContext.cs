using System;
using Microsoft.EntityFrameworkCore;
using PET.Domain.Models;

namespace PET.Ef.DbContexts
{
    public class UserDbContext : DbContext
    {
        [Obsolete("UserDbContext is deprecated, use AnimalDbContext")]
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}