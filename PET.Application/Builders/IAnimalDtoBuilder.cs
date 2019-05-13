﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PET.Application.DTOs;
using PET.Domain;
using PET.Domain.Models;

namespace PET.Application.Builders
{
    public class AnimalBuilder : IAnimalBuilder
    {
        public Animal Build(AnimalSaveDto animal, IEnumerable<string> waysToFile)
        {
            return new Animal
            {
                Id = Guid.NewGuid(),
                AnimalType = animal.AnimalType,
                BDate = animal.BDate,
                Description = animal.Description,
                Name = animal.Name,
                Sex = (Sex) animal.Sex,
                Files = waysToFile.Select(w => new File{Id = Guid.NewGuid(), WayToFile = w}).ToArray()
            };
        }
    }

    public interface IAnimalBuilder
    {
        Animal Build(AnimalSaveDto animal, IEnumerable<string> waysToFile);
    }

    public interface IAnimalDtoBuilder
    {
        AnimalDto Build(Animal animal);
    }

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
                Name = animal.Name,
                Sex = (SexM) animal.Sex,
                WayToFiles = animal.Files.Select(f => f.WayToFile).ToArray()
            };
        }
    }
}
