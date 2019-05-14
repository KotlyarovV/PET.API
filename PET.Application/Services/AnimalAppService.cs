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

        Task RemoveAsync(T entity);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(ISpecification<T> spec);

        Task Update(T entity);
    }

    public class FileAppService
    {
        private readonly IFileStorageService fileStorageService;

        public FileAppService(IFileStorageService fileStorageService)
        {
            this.fileStorageService = fileStorageService;
        }

        public async Task<MemoryStream> Get(string file)
        {
            var memoryStream = new MemoryStream();
            await fileStorageService.Load(memoryStream, file);
            return memoryStream;
        }
    }

    public class AnimalAppService
    {
        private readonly IDataService<Animal> animalDataService;
        private readonly IAnimalDtoBuilder animalDtoBuilder;
        private readonly IAnimalBuilder animalBuilder;
        private readonly IFileStorageService fileStorageService;
        private readonly IFileBuilder fileBuilder;

        public AnimalAppService(IDataService<Animal> animalDataService,
            IAnimalDtoBuilder animalDtoBuilder,
            IAnimalBuilder animalBuilder,
            IFileStorageService fileStorageService,
            IFileBuilder fileBuilder)
        {
            this.animalDataService = animalDataService;
            this.animalDtoBuilder = animalDtoBuilder;
            this.animalBuilder = animalBuilder;
            this.fileStorageService = fileStorageService;
            this.fileBuilder = fileBuilder;
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
            var files = animalSaveDto.Files
                .Select(f  => new {FileDTO = f, File = fileBuilder.Build(f)})
                .ToArray();

            await Task.WhenAll(files.Select(f => fileStorageService.Save(
                new MemoryStream(Convert.FromBase64String(f.FileDTO.FileInBase64)),
                f.File.WayToFile)));

            var animal = animalBuilder.Build(animalSaveDto, files.Select(f => f.File).ToArray());
            await animalDataService.AddAsync(animal);
            return animal.Id;
        }

        public async Task Update(Guid id, AnimalUpdateDto animalUpdateDto)
        {

            var files = animalUpdateDto.Files
                .Select(f => new { FileDTO = f, File = fileBuilder.Build(f) })
                .ToArray();

            await Task.WhenAll(files.Select(f => fileStorageService.Save(
                new MemoryStream(Convert.FromBase64String(f.FileDTO.FileInBase64)),
                f.File.WayToFile)));

            var animal = animalBuilder.Build(id, animalUpdateDto, files.Select(f => f.File).ToArray());
            await animalDataService.Update(animal);

        }

        public async Task Delete(Guid id)
        {
            var animal = await animalDataService.GetAsync(new AnimalIdSpecification(id));
            await animalDataService.RemoveAsync(animal);
        }
    }
}
