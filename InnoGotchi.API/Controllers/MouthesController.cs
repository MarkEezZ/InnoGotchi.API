using AutoMapper;
using InnoGotchi.API.Contracts;
using InnoGotchi.API.Entities.DataTransferObjects;
using InnoGotchi.API.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.API.Controllers
{
    [Route("api/mouths")]
    [ApiController]
    public class MouthesController: ControllerBase
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;

        public MouthesController(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetMouths()
        {
            var mouth = repository.Mouth.GetAllMouthes(trackChanges: false);
            if (mouth != null)
            {
                return Ok(mouth);
            }
            return NotFound("Mouths are not found.");
        }

        [HttpPost]
        public IActionResult CreateMouth([FromBody]BodyPartDto mouthToCreate)
        {
            var sameMouth = repository.Mouth.GetMouthByName(mouthToCreate.Name, trackChanges: false);
            if (sameMouth == null)
            {
                var mouth = mapper.Map<Mouth>(mouthToCreate);
                repository.Mouth.CreateMouth(mouth);
                repository.Save();

                return Ok("Mouth was successfuly added.");
            }
            return BadRequest($"Mouth with name \"{mouthToCreate.Name}\" already exists.");
        }

        [HttpDelete]
        public IActionResult DeleteMouth([FromBody]BodyPartDto mouthToDelete)
        {
            var mouth = repository.Mouth.GetMouthByName(mouthToDelete.Name, trackChanges: false);
            if (mouth != null)
            {
                repository.Mouth.DeleteMouth(mouth);
                repository.Save();
                return Ok("Mouth was successfuly deleted.");
            }
            return BadRequest($"There is no mouth with name \"{mouthToDelete.Name}\".");
        }
    }
}
