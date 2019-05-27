using System.Linq;
using PET.Application.DTOs;
using PET.Domain.Models;
using PET.Domain.Specifications;

namespace PET.Application.Builders
{
    public class AnimalSpecificationBuilder : IAnimalSpecificationBuilder
    {
        public AnimalSpecification Build(AnimalSpecDto animalSpecDto)
        {
            return new AnimalSpecification
            {
                BDateTo = animalSpecDto.BDateTo,
                BDateFrom = animalSpecDto.BDateFrom,
                Kinds = animalSpecDto.Kinds?.ToArray(),
                Sex = (Sex?)animalSpecDto.Sex
            };
        }
    }
}