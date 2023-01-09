using InnoGotchi.API.Entities.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using InnoGotchi.API.Contracts;
using InnoGotchi.API.Entities.DataTransferObjects;

namespace InnoGotchi.API.Controllers
{
    [ApiController]
    [Route("api/farms/{farmName}/pets")]
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
        public IActionResult GetFarmPets([FromRoute] string farmName)
        {
            var farm = repository.Farm.GetFarmByFarmName(farmName, trackChanges: false);
            if (farm != null)
            {
                var pets = repository.Pet.GetAllFarmPets(farm.Id, trackChanges: false);
                if (pets != null)
                {
                    return Ok(pets);
                }
                return Ok("There is no pets on the farm.");
            }
            return BadRequest($"There is no farm with name \"{farmName}\".");
        }

        [HttpGet("{petName}", Name = "GetPetByName")]
        public IActionResult GetPetByName([FromRoute] string petName)
        {
            var pet = repository.Pet.GetPetByName(petName, trackChanges: false);
            if (pet != null)
            {
                return Ok(pet);
            }
            return BadRequest($"There is no pet with name \"{petName}\" on this farm.");
        }

        [HttpPost("constructor")]
        public IActionResult CreatePet([FromRoute] string farmName, [FromBody] PetDto petData)
        {
            var samePet = repository.Pet.GetPetByName(petData.Name, trackChanges: false);
            if (samePet == null)
            {
                Pet pet = mapper.Map<Pet>(petData);

                var farm = repository.Farm.GetFarmByFarmName(farmName, trackChanges: false);
                if (farm != null)
                {
                    pet.FarmId = farm.Id;
                    pet.TimeOfCreating = DateTime.Now;
                    repository.Pet.CreatePet(pet);  
                    repository.Save();

                    var petFromDb = repository.Pet.GetPetByName(petData.Name, trackChanges: false);
                    return CreatedAtRoute("GetPetByName", routeValues: new { petName = petFromDb.Name }, value: petFromDb);
                }
            }
            return BadRequest("This name is already taken.");
        }

        [HttpPut("{petName}/update")]
        public IActionResult UpdatePet([FromRoute] string farmName, [FromRoute] string petName, [FromBody] PetDto petData)
        {
            var farm = repository.Farm.GetFarmByFarmName(farmName, trackChanges: false);
            if (farm != null)
            {
                var pet = repository.Pet.GetPetByName(petName, trackChanges: false);
                if (pet != null)
                {
                    if (pet.FarmId == farm.Id)
                    {
                        pet.Name = petData.Name;
                        pet.MouthId = petData.MouthId;
                        pet.NoseId = petData.NoseId;
                        pet.BodyId = petData.BodyId;
                        pet.EyesId = petData.EyesId;

                        repository.Pet.UpdatePet(pet);
                        repository.Save();

                        return Ok("Pet was sucsessfuly updated.");
                    }
                    return Forbid("You have no rights to update pets from not your farm.");
                }
                return NotFound($"There is no pet with name \"{petName}\".");
            }
            return NotFound($"Farm with name {farmName} is not found.");
        }

        [HttpDelete("remove")]
        public IActionResult DeletePet([FromRoute] string farmName, [FromBody] PetDto petData)
        {
            var farm = repository.Farm.GetFarmByFarmName(farmName, trackChanges: false);
            if (farm != null)
            {
                var pet = repository.Pet.GetPetByName(petData.Name, trackChanges: false);
                if (pet != null)
                {
                    if (pet.FarmId == farm.Id)
                    {
                        repository.Pet.DeletePet(pet);
                        repository.Save();
                        return Ok("Pet was sucsessfuly deleted");
                    }
                    return NotFound($"There is no pet with name \"{pet.Name}\" on the farm.");
                }
                return BadRequest($"There is no pet with name \"{petData.Name}\".");
            }
            return BadRequest($"There is no farm with name \"{farmName}\".");
        }
    }
}