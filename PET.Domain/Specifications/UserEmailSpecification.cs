using System;
using System.Linq.Expressions;
using PET.Domain.Models;

namespace PET.Domain.Specifications
{
    public class UserEmailSpecification : ISpecification<User>
    {
        public UserEmailSpecification(string email)
        {
            IsSatisfiedBy = u => u.Email == email;
        }

        public Expression<Func<User, bool>> IsSatisfiedBy { get; }
    }
}