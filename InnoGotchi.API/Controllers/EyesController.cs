using AutoMapper;
using InnoGotchi.API.Contracts;
using InnoGotchi.API.Entities.DataTransferObjects;
using InnoGotchi.API.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace InnoGotchi.API.Controllers
{
    [Route("innogotchi/eyes")]
    [ApiController]
    [Authorize]
    public class EyesController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;

        public EyesController(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllEyes()
        {
            var eyes = repository.Eyes.GetAllEyes(trackChanges: false);
            if (eyes != null)
            {
                return Ok(eyes);
            }
            return Ok();
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public IActionResult CreateEyes([FromBody] BodyPartDto eyesToCreate)
        {
            var sameEyes = repository.Eyes.GetEyesByName(eyesToCreate.Name, trackChanges: false);
            if (sameEyes == null)
            {
                var eyes = mapper.Map<Eyes>(eyesToCreate);
                repository.Eyes.CreateEyes(eyes);
                repository.Save();

                return Ok("Eyes was successfuly created.");
            }
            return BadRequest($"Eyes with name \"{eyesToCreate.Name}\" already exists.");
        }

        [HttpDelete]
        [Authorize(Policy = "Admin")]
        public IActionResult DeleteEyes([FromBody] BodyPartDto eyesToDelete)
        {
            var eyes = repository.Eyes.GetEyesByName(eyesToDelete.Name, trackChanges: false);
            if (eyes != null)
            {
                repository.Eyes.DeleteEyes(eyes);
                repository.Save();
                return Ok("Eyes was successfuly deleted.");
            }
            return BadRequest($"There is no eyes with name \"{eyesToDelete.Name}\".");
        }
    }
}
