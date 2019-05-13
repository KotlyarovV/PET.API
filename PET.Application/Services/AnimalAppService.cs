using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PET.Application.Builders;
using PET.Application.DTOs;
using PET.Domain;
using PET.Domain.Models;
using PET.Domain.Specifications;

namespace PET.Application.Services
{
    public interface IFileStorageService
    {
        Task Save(MemoryStream memoryStream, string wayToFile);

        Task Load(MemoryStream memoryStream, string wayToFile);

        Task Delete(string wayToFile);
    }

    public interface IDataService<T>
    {
        Task<T> AddAsync(T entity);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(ISpecification<T> spec);

        Task Update(T entity);
    }

    public class AnimalAppService
    {
        private readonly IDataService<Animal> animalDataService;
        private readonly IAnimalDtoBuilder animalDtoBuilder;
        private readonly IAnimalBuilder animalBuilder;
        private readonly IFileStorageService fileStorageService;

        public AnimalAppService(IDataService<Animal> animalDataService,
            IAnimalDtoBuilder animalDtoBuilder,
            IAnimalBuilder animalBuilder,
            IFileStorageService fileStorageService)
        {
            this.animalDataService = animalDataService;
            this.animalDtoBuilder = animalDtoBuilder;
            this.animalBuilder = animalBuilder;
            this.fileStorageService = fileStorageService;
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
            var animal = await animalDataService.GetAsync(new AnimalIdSpecification(id));
            var animalDto = animalDtoBuilder.Build(animal);
            return animalDto;
        }

        public async Task<Guid> Create(AnimalSaveDto animalSaveDto)
        {
            var f
        }
    }
}
