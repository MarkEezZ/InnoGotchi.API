using InnoGotchi.API.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using InnoGotchi.API.Contracts;
using InnoGotchi.API.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;

namespace InnoGotchi.API.Controllers
{
    [ApiController]
    [Route("innogotchi/farms/{farmName}/pets")]
    public class PetsController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;

        public PetsController(IRepositoryManager repository, IMapper mapper, ILoggerManager logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetFarmPets([FromRoute] string farmName)
        {
            var farm = repository.Farm.GetFarmByFarmName(farmName, trackChanges: false);
            var pets = repository.Pet.GetAllFarmPets(farm.Id, trackChanges: false);
            if (pets != null)
            {
                return Ok(pets);
            }
            return Ok();
        }

        [HttpGet("{petName}", Name = "GetPetByName")]
        [Authorize]
        public IActionResult GetPetByName([FromRoute] string petName)
        {
            var pet = repository.Pet.GetPetByName(petName, trackChanges: false);
            if (pet != null)
            {
                return Ok(pet);
            }
            return NotFound($"Pet with name \"{petName}\" is not found.");
        }

        [HttpPatch("{petName}/feed")]
        [Authorize]
        public IActionResult FeedPet([FromRoute] string petName)
        {
            var pet = repository.Pet.GetPetByName(petName, trackChanges: false);
            if (pet != null)
            {
                pet.LastEatTime = DateTime.Now;
                repository.Pet.UpdatePet(pet);
                repository.Save();
                return Ok();
            }
            return NotFound($"Pet with name \"{petName}\" is not found.");
        }

        [HttpPatch("{petName}/water")]
        [Authorize]
        public IActionResult WaterPet([FromRoute] string petName)
        {
            var pet = repository.Pet.GetPetByName(petName, trackChanges: false);
            if (pet != null)
            {
                pet.LastDrinkTime = DateTime.Now;
                repository.Pet.UpdatePet(pet);
                repository.Save();
                return Ok();
            }
            return NotFound($"Pet with name \"{petName}\" is not found.");
        }

        [HttpPatch("{petName}/cure")]
        [Authorize]
        public IActionResult CurePet([FromRoute] string petName)
        {
            var pet = repository.Pet.GetPetByName(petName, trackChanges: false);
            if (pet != null)
            {
                pet.LastHealthTime = DateTime.Now;
                repository.Pet.UpdatePet(pet);
                repository.Save();
                return Ok();
            }
            return NotFound($"Pet with name \"{petName}\" is not found.");
        }

        [HttpPatch("{petName}/cheer-up")]
        [Authorize]
        public IActionResult CheerUpPet([FromRoute] string petName)
        {
            var pet = repository.Pet.GetPetByName(petName, trackChanges: false);
            if (pet != null)
            {
                pet.LastMoodTime = DateTime.Now;
                repository.Pet.UpdatePet(pet);
                repository.Save();
                return Ok();
            }
            return NotFound($"Pet with name \"{petName}\" is not found.");
        }

        [HttpPost("constructor")]
        [Authorize]
        public IActionResult CreatePet([FromRoute] string farmName, [FromBody] PetDto petData)
        {
            UserClaims? userClaims = (UserClaims?)HttpContext.Items["User"];

            if (farmName == userClaims.OwnFarm)
            {
                var samePet = repository.Pet.GetPetByName(petData.Name, trackChanges: false);
                if (samePet == null)
                {
                    var farm = repository.Farm.GetFarmByFarmName(farmName, trackChanges: false);

                    Pet pet = mapper.Map<Pet>(petData);
                    DateTime now = DateTime.Now;

                    pet.LastDrinkTime = now;
                    pet.LastEatTime = now;
                    pet.LastHealthTime = now;
                    pet.LastMoodTime = now;
                    pet.FarmId = farm.Id;
                    pet.TimeOfCreating = DateTime.Now;

                    repository.Pet.CreatePet(pet);
                    repository.Save();

                    var petFromDb = repository.Pet.GetPetByName(petData.Name, trackChanges: false);
                    return CreatedAtRoute("GetPetByName", routeValues: new { petName = petFromDb.Name }, value: petFromDb);
                }
                return BadRequest("This name is already taken.");
            }
            return Forbid("You have no rights to create a pet on someone else's farm");
        }

        [HttpDelete("delete")]
        [Authorize]
        public IActionResult DeletePet([FromRoute] string farmName, [FromBody] string petName)
        {
            UserClaims? userClaims = (UserClaims?)HttpContext.Items["User"];

            if (farmName == userClaims.OwnFarm)
            {
                var pet = repository.Pet.GetPetByName(petName, trackChanges: false);
                repository.Pet.DeletePet(pet);
                repository.Save();

                return Ok("Pet was successfuly deleted.");
            }
            return Forbid("You have no rights to delete a pet from someone else's farm.");
        }
    }
}