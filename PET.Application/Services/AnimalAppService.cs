using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PET.Application.Builders;
using PET.Application.DTOs;
using PET.Domain.Models;
using PET.Domain.Specifications;

namespace PET.Application.Services
{
    public class AnimalAppService
    {
        private readonly IDataService<Animal> animalDataService;
        private readonly IAnimalDtoBuilder animalDtoBuilder;
        private readonly IAnimalBuilder animalBuilder;
        private readonly IFileStorageService fileStorageService;
        private readonly IFileBuilder fileBuilder;
        private readonly IAnimalSpecificationBuilder animalSpecificationBuilder;

        public AnimalAppService(IDataService<Animal> animalDataService,
            IAnimalDtoBuilder animalDtoBuilder,
            IAnimalBuilder animalBuilder,
            IFileStorageService fileStorageService,
            IFileBuilder fileBuilder,
            IAnimalSpecificationBuilder animalSpecificationBuilder)
        {
            this.animalDataService = animalDataService;
            this.animalDtoBuilder = animalDtoBuilder;
            this.animalBuilder = animalBuilder;
            this.fileStorageService = fileStorageService;
            this.fileBuilder = fileBuilder;
            this.animalSpecificationBuilder = animalSpecificationBuilder;
        }

        public async Task<IEnumerable<AnimalDto>> GetAll()
        {
            var animals = await animalDataService.GetAllAsync();
            var animalsDto = animals
                .Select(animalDtoBuilder.Build)
                .ToArray();

            return animalsDto;
        }

        public async Task<AnimalDto> Get(Guid id)
        {
            var animal = await animalDataService.GetAsync(new AnimalSpecification {Id = id});

            if (animal == null)
            {
                return null;
            }

            var animalDto = animalDtoBuilder.Build(animal);

            return animalDto;
        }

        public async Task<IEnumerable<AnimalDto>> Get(AnimalSpecDto animalSpecDto)
        {
            var animalSpec = animalSpecificationBuilder.Build(animalSpecDto);
            var animals = await animalDataService.GetAllAsync(animalSpec);
            var animalsDto = animals.Select(animalDtoBuilder.Build)
                .ToArray();

            return animalsDto;
        }

        public async Task<Guid> Create(AnimalSaveDto animalSaveDto)
        {
            var files = animalSaveDto.Files
                .Select(f => new {FileDTO = f, File = fileBuilder.Build(f)})
                .ToArray();

            await Task.WhenAll(files.Select(f => fileStorageService.Save(
                new MemoryStream(Convert.FromBase64String(f.FileDTO.FileInBase64)),
                f.File.WayToFile)));

            var animal = animalBuilder.Build(animalSaveDto, files.Select(f => f.File)
                .ToArray());
            await animalDataService.AddAsync(animal);

            return animal.Id;
        }

        public async Task Update(Guid id, AnimalUpdateDto animalUpdateDto)
        {
            var files = animalUpdateDto.Files
                .Select(f => new {FileDTO = f, File = fileBuilder.Build(f)})
                .ToArray();

            await Task.WhenAll(files.Select(f => fileStorageService.Save(
                new MemoryStream(Convert.FromBase64String(f.FileDTO.FileInBase64)),
                f.File.WayToFile)));

            var animal = animalBuilder.Build(id, animalUpdateDto, files.Select(f => f.File)
                .ToArray());
            await animalDataService.Update(animal);
        }

        public async Task Delete(Guid id)
        {
            var animal = await animalDataService.GetAsync(new AnimalSpecification {Id = id});
            await animalDataService.RemoveAsync(animal);
        }
    }
}