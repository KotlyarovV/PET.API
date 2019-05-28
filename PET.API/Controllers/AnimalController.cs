using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PET.Application.Builders;
using PET.Application.DTOs;
using PET.Application.Services;
using PET.API.Services.Authorization;

namespace PET.API.Controllers
{
    [Route("animal")]
    public class AnimalController : Controller
    {
        private readonly AnimalAppService animalAppService;
        private readonly UserAppService userAppService;


        public AnimalController(AnimalAppService animalAppService, UserAppService userAppService)
        {
            this.animalAppService = animalAppService;
            this.userAppService = userAppService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<AnimalDto>> GetAll()
        {
            return await animalAppService.GetAll();
        }

        [HttpGet]
        [Route("filtered")]
        public async Task<IEnumerable<AnimalDto>> GetFiltered(AnimalSpecDto animalSpecDto)
        {
            return await animalAppService.Get(animalSpecDto);
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
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            var user = await userAppService.Get(userEmail);

            return await animalAppService.Create(animalSaveDto, user);
        }

        [HttpPost]
        [Route("{id}")]
        [Authorize(Policy = nameof(MustOwnAnimalRequirement))]
        public async Task Update(Guid id, [FromBody] AnimalUpdateDto animalUpdateDto)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            var user = await userAppService.Get(userEmail);

            await animalAppService.Update(id, animalUpdateDto, user);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Policy = nameof(MustOwnAnimalRequirement))]
        public async Task Delete(Guid id)
        {
            await animalAppService.Delete(id);
        }
    }
}
