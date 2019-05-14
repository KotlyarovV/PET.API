using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PET.Application.Services;
using PET.Domain.Models;
using PET.Domain.Specifications;
using PET.Infrastructure;

namespace PET.API
{
    public static class DataExtensions
    {
        public static void PrepareDB(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var petsRepository = scope.ServiceProvider.GetRequiredService<IDataService<Animal>>();
                petsRepository.SeedWithSampleAnimals();
            }
        }

        private static void SeedWithSampleAnimals(this IDataService<Animal> repository)
        {
            var animalsFromRepo = repository.GetAllAsync()
                .Result;

            foreach (var animal in animalsFromRepo)
            {
                repository.RemoveAsync(animal)
                    .Wait();
            }

            var animals = new[]
            {
                new Animal
                {
                    AnimalType = "Cat",
                    BDate = DateTime.Today,
                    Description = "Кошка картошка",
                    Id = Guid.NewGuid(),
                    Name = "Пуговка",
                    Sex = Sex.Female
                },

                new Animal
                {
                    AnimalType = "Cat",
                    BDate = DateTime.Today - TimeSpan.FromDays(30),
                    Description = "Кот обормот",
                    Id = Guid.NewGuid(),
                    Name = "Вася",
                    Sex = Sex.Male
                },

                new Animal
                {
                    AnimalType = "Cat",
                    BDate = DateTime.Today,
                    Description = "Спокойная кошка",
                    Id = Guid.NewGuid(),
                    Name = "Маруся",
                    Sex = Sex.Female
                }
            };

            foreach (var animal in animals)
            {
                repository.AddAsync(animal).Wait();
            }
        }
    }
}