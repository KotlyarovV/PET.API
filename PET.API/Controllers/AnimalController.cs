using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PET.Application.DTOs;
using PET.Application.Services;

namespace PET.API.Controllers
{
    [Route("animal")]
    public class AnimalController : Controller
    {
        private readonly AnimalAppService animalAppService;

        public AnimalController(AnimalAppService animalAppService)
        {
            this.animalAppService = animalAppService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<AnimalDto>> GetAll()
        {
            return await animalAppService.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<AnimalDto> Get(Guid id)
        {
            return await animalAppService.Get(id);
        }
    }
}
