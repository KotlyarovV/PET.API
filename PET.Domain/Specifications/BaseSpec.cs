using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PET.Domain.Specifications
{
    public class BaseSpec<T> : ISpecification<T>
    {
        public static Expression<Func<T, bool>> CombineWithOr<T>(params Expression<Func<T, bool>>[] filters)
        {
            var first = filters.First();
            var param = first.Parameters.First();
            var body = first.Body;

            foreach (var other in filters.Skip(1))
            {
                var replacer = new ReplaceParameter
                {
                    OriginalParameter = other.Parameters.First(),
                    NewParameter = param
                };
                // We need to replace the original expression parameter with the result parameter
                body = Expression.AndAlso(body, replacer.Visit(other.Body));
            }

            return Expression.Lambda<Func<T, bool>>(
                body,
                param
            );
        }

        //public Expression<Func<Animal, bool>> IsSatisfiedBy { get; }
        public Expression<Func<T, bool>> IsSatisfiedBy {
            get
            {
                var conditions = Conditions.Where(c => c.NeedToUse()).Select(c => c.Condition).ToArray();
                return CombineWithOr(conditions);
            }
        }

        public BaseSpec()
        {
            //IsSatisfiedBy = CombineWithOr(Conditions.Where(c => c.NeedToUse()).Select(c => c.Condition).ToArray());
        }
        
        protected List<(Func<bool> NeedToUse, Expression<Func<T, bool>> Condition)> Conditions { get; set; } = new List<(Func<bool>, Expression<Func<T, bool>>)>();
    }
}