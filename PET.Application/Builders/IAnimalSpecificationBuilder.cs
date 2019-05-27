using PET.Application.DTOs;
using PET.Domain.Specifications;

namespace PET.Application.Builders
{
    public interface IAnimalSpecificationBuilder
    {
        AnimalSpecification Build(AnimalSpecDto animalSpecDto);
    }
}