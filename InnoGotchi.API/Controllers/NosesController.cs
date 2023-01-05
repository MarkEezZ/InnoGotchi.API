using AutoMapper;
using InnoGotchi.API.Contracts;
using InnoGotchi.API.Entities.DataTransferObjects;
using InnoGotchi.API.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InnoGotchi.API.Controllers
{
    [Route("api/noses")]
    [ApiController]
    public class NosesController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;

        public NosesController(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetNoses()
        {
            var noses = repository.Nose.GetAllNoses(trackChanges: false);
            if (noses != null)
            {
                return Ok(noses);
            }
            return NotFound("Noses are not found.");
        }

        [HttpPost]
        public IActionResult CreateNose([FromBody]BodyPartDto noseToCreate)
        {
            Console.WriteLine($"\n\n{JsonSerializer.Serialize(noseToCreate)}\n\n");
            var sameNose = repository.Nose.GetNoseByName(noseToCreate.Name, trackChanges: false);
            if (sameNose == null)
            {
                var nose = mapper.Map<Nose>(noseToCreate);
                repository.Nose.CreateNose(nose);
                repository.Save();

                return Ok("Nose was successfuly added.");
            }
            return BadRequest($"Nose with name \"{noseToCreate.Name}\" already exists.");
        }

        [HttpDelete]
        public IActionResult DeleteNose([FromBody]BodyPartDto noseToDelete)
        {
            var nose = repository.Nose.GetNoseByName(noseToDelete.Name, trackChanges: false);
            if (nose != null)
            {
                repository.Nose.DeleteNose(nose);
                repository.Save();
                return Ok("Nose was successfuly deleted.");
            }
            return BadRequest($"There is no nose with name \"{noseToDelete.Name}\".");
        }
    }
}
