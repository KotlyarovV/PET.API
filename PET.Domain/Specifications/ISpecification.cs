using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using PET.Domain.Models;

namespace PET.Domain.Specifications
{
    public class AnimalSpecification : BaseSpec<Animal>
    {
        public Guid? Id { get; set; }

        public string[] Kinds { get; set; }

        public DateTime?  BDateFrom { get; set; }

        public DateTime? BDateTo { get; set; }

        public Sex? Sex { get; set; }


        public AnimalSpecification()
        {
            Conditions
                .Add((() => Id.HasValue, (a) => a.Id == Id.Value));
            Conditions
                .Add((() => Kinds != null, (a) => Kinds.Contains(a.Kind)));
            Conditions
                .Add((() => BDateFrom.HasValue, (a) => a.BDate >= BDateFrom.Value));
            Conditions
                .Add((() => BDateTo.HasValue, (a) => a.BDate <= BDateTo.Value));
            Conditions
                .Add((() => Sex.HasValue, (a) => a.Sex == Sex.Value));
        }

        public Expression<Func<Animal, bool>> IsSatisfiedBy { get; }
    }

    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> IsSatisfiedBy { get; }
    }
}
