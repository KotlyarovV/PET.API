using System.Text;
using PET.Application.DTOs;
using PET.Domain;
using PET.Domain.Models;

namespace PET.Application.Builders
{
    public interface IAnimalDtoBuilder
    {
        AnimalDto Build(Animal animal);
    }
}
