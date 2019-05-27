using System;
using System.Collections.Generic;
using PET.Application.DTOs;
using PET.Domain.Models;

namespace PET.Application.Builders
{
    public interface IAnimalBuilder
    {
        Animal Build(AnimalSaveDto animal, IEnumerable<File> files);
        Animal Build(Guid id, AnimalUpdateDto animal, IEnumerable<File> files);
    }
}