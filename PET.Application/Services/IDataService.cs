using System.Collections.Generic;
using System.Threading.Tasks;
using PET.Domain.Specifications;

namespace PET.Application.Services
{
    public interface IDataService<T>
    {
        Task<T> AddAsync(T entity);

        Task RemoveAsync(T entity);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(ISpecification<T> spec);

        Task Update(T entity);
    }
}