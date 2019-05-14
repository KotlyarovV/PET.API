using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PET.Application.Services;
using PET.Domain.Models;
using PET.Ef.DbContexts;

namespace PET.Infrastructure
{
    public class UserDataService : Repository<User>, IDataService<User>
    {
        public UserDataService(UserDbContext context) : base(context)
        {
        }

        protected override IQueryable<User> ConfigureQuery()
        {
            return dbSet;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await ConfigureQuery().ToArrayAsync();
        }
    }
}