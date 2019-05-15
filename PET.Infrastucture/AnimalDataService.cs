using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PET.Application.Services;
using PET.Domain.Models;
using PET.Ef.DbContexts;

namespace PET.Infrastructure
{
    public class AnimalDataService : Repository<Animal>, IDataService<Animal>
    {
        public AnimalDataService(AnimalDbContext context) : base(context)
        {
        }

        protected override IQueryable<Animal> ConfigureQuery()
        {
            return dbSet
                .Include(a => a.Files);
        }

        public async Task<IEnumerable<Animal>> GetAllAsync()
        {
            return await ConfigureQuery().ToArrayAsync();
        }
    }
}