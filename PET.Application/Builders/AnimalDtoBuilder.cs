using System.Linq;
using PET.Application.DTOs;
using PET.Domain.Models;

namespace PET.Application.Builders
{
    public class AnimalDtoBuilder : IAnimalDtoBuilder
    {
        public AnimalDto Build(Animal animal)
        {
            return new AnimalDto
            {
                AnimalType = animal.AnimalType,
                BDate = animal.BDate,
                Description = animal.Description,
                Id = animal.Id,
                Passport = animal.Passport,
                Sterilization = animal.Sterilization,
                Vaccination = animal.Vaccination,
                Kind = animal.Kind,
                Name = animal.Name,
                Sex = (SexM) animal.Sex,
                WayToFiles = animal.Files.Select(f => f.WayToFile).ToArray()
            };
        }
    }
}