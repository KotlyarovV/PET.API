using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PET.Application.DTOs;
using PET.Application.Services;

namespace PET.API.Controllers
{

    [Route("files")]
    public class FileController : Controller
    {
        private readonly FileAppService fileAppService;

        public FileController(FileAppService fileAppService)
        {
            this.fileAppService = fileAppService;
        }

        [HttpGet]
        [Route("{fileName}")]
        public async Task<IActionResult> Get(string fileName)
        {
            var memoryStream = await fileAppService.Get(fileName);
            return new FileContentResult(memoryStream.ToArray(), "application/octet-stream")
            {
                FileDownloadName = fileName
            };
        }
    }

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
        public async Task<Guid> Create([FromBody] AnimalSaveDto animalSaveDto)
        {
            return await animalAppService.Create(animalSaveDto);
        }

        [HttpPost]
        [Route("{id}")]
        public async Task Update(Guid id, [FromBody] AnimalUpdateDto animalUpdateDto)
        {
            await animalAppService.Update(id, animalUpdateDto);
        }

        [HttpDelete]
        [Route("id")]
        public async Task Delete(Guid id)
        {
            await animalAppService.Delete(id);
        }
    }
}
