using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<Guid> Create([FromBody] AnimalSaveDto animalSaveDto)
        {
            return await animalAppService.Create(animalSaveDto);
        }

        [HttpPost]
        [Route("{id}")]
        [Authorize]
        public async Task Update(Guid id, [FromBody] AnimalUpdateDto animalUpdateDto)
        {
            await animalAppService.Update(id, animalUpdateDto);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task Delete(Guid id)
        {
            await animalAppService.Delete(id);
        }
    }
}
