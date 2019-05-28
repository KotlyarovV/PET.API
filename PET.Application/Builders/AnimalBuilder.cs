using System;
using System.Collections.Generic;
using System.Linq;
using PET.Application.DTOs;
using PET.Domain.Models;

namespace PET.Application.Builders
{
    public class AnimalBuilder : IAnimalBuilder
    {
        public Animal Build(AnimalSaveDto animal, IEnumerable<File> files, User owner)
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
                Sex = (Sex)animal.Sex,
                Files = files.ToArray(),
                UserId = owner.Id
            };
        }

        public Animal Build(Guid id, AnimalUpdateDto animal, IEnumerable<File> files, User owner)
        {
            var result = owner.Animals.First(a => a.Id == id);

            result.Id = id;
            result.AnimalType = animal.AnimalType;
            result.Passport = animal.Passport;
            result.Sterilization = animal.Sterilization;
            result.Vaccination = animal.Vaccination;
            result.Kind = animal.Kind;
            result.BDate = animal.BDate;
            result.Description = animal.Description;
            result.Name = animal.Name;
            result.Sex = (Sex)animal.Sex;
            result.Files = files.ToArray();
            result.UserId = owner.Id;

            return result;
        }
    }
}