using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using PET.Domain.Models;

namespace PET.Domain.Specifications
{
    public class AnimalIdSpecification : ISpecification<Animal>
    {
        public AnimalIdSpecification(Guid id)
        {
            IsSatisfiedBy = a => a.Id == id;
        }

        public Expression<Func<Animal, bool>> IsSatisfiedBy { get; }
    }

    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> IsSatisfiedBy { get; }
    }
}
