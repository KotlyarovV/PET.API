using System;
using System.Collections.Generic;
using System.Linq;
using PET.Application.DTOs;
using PET.Domain.Models;

namespace PET.Application.Builders
{
    public class AnimalBuilder : IAnimalBuilder
    {
        public Animal Build(AnimalSaveDto animal, IEnumerable<File> files)
        {
            return new Animal
            {
                Id = Guid.NewGuid(),
                AnimalType = animal.AnimalType,
                BDate = animal.BDate,
                Description = animal.Description,
                Name = animal.Name,
                Passport = animal.Passport,
                Sterilization = animal.Sterilization,
                Vaccination = animal.Vaccination,
                Kind = animal.Kind,
                Sex = (Sex) animal.Sex,
                Files = files.ToArray()
            };
        }

        public Animal Build(Guid id, AnimalUpdateDto animal, IEnumerable<File> files)
        {
            return new Animal
            {
                Id = id,
                AnimalType = animal.AnimalType,
                Passport = animal.Passport,
                Sterilization = animal.Sterilization,
                Vaccination = animal.Vaccination,
                Kind = animal.Kind,
                BDate = animal.BDate,
                Description = animal.Description,
                Name = animal.Name,
                Sex = (Sex)animal.Sex,
                Files = files.ToArray()
            };
        }
    }
}