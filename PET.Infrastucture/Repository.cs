using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PET.Application.Services;
using PET.Domain.Models;
using PET.Domain.Specifications;
using PET.Ef.DbContexts;

namespace PET.Infrastructure
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext context;
        protected readonly DbSet<TEntity> dbSet;

        protected Repository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        protected abstract IQueryable<TEntity> ConfigureQuery();

        public Task<TEntity> GetAsync(ISpecification<TEntity> spec)
        {
            return ConfigureQuery().Where(spec.IsSatisfiedBy).SingleAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> spec)
        {
            return await ConfigureQuery().Where(spec.IsSatisfiedBy).ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity item)
        {
            await dbSet.AddAsync(item);
            await context.SaveChangesAsync();
            return item;
        }

        public async Task Update(TEntity item)
        {
            context.Entry(item).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(TEntity item)
        {
            dbSet.Remove(item);
            await context.SaveChangesAsync();
        }
    }

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

    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity item);

        Task<TEntity> GetAsync(ISpecification<TEntity> spec);

        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification);

        Task RemoveAsync(TEntity item);

        Task Update(TEntity item);
    }
}
