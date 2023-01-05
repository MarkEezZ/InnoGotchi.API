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
    [Route("api/farms/{farmName}")]
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

        [HttpGet("pets")]
        public IActionResult GetFarmPets([FromRoute]string farmName)
        {
            var farm = repository.Farm.GetFarmByFarmName(farmName, trackChanges: false);
            if (farm != null)
            {
                var pets = repository.Pet.GetAllFarmPets(farm.Id, trackChanges: false);
                if (pets != null)
                {
                    return Ok(pets);
                }
                return Ok();
            }
            return BadRequest($"There is no farms with name \"{farmName}\".");
        }

        [HttpGet("pets/{petName}", Name = "GetPetByName")]
        public IActionResult GetPetByName([FromRoute]string petName)
        {
            var pet = repository.Pet.GetPetByName(petName, trackChanges: false);
            if (pet != null)
            {
                return Ok(pet);
            }
            return BadRequest($"There is no pets with name \"{petName}\" on this farm.");
        }

        [HttpPost("pets/constructor")]
        public IActionResult CreatePet([FromRoute]string farmName, [FromBody]PetDto petData)
        {
            var samePet = repository.Pet.GetPetByName(petData.Name, trackChanges: false);
            if (samePet == null)
            {
                var pet = mapper.Map<Pet>(petData);

                var farm = repository.Farm.GetFarmByFarmName(farmName, trackChanges: false);
                if (farm != null)
                {
                    pet.FarmId = farm.Id;
                    repository.Save();

                    var petFromDb = repository.Pet.GetPetByName(petData.Name, trackChanges: false);
                    var petToReturn = mapper.Map<PetToReturnDto>(petFromDb);
                    return CreatedAtRoute("GetPetByName", routeValues: new { name = petToReturn.Name }, value: petToReturn);
                }
            }
            return BadRequest("This name is already taken.");
        }

        [HttpDelete("pets/remove")]
        public IActionResult DeletePet([FromBody]PetToReturnDto petInfo)
        {
            var pet = repository.Pet.GetPetByName(petInfo.Name, trackChanges: false);
            if (pet != null)
            {
                
            }
            return BadRequest($"There is no pets with name \"{petInfo.Name}\".");
        }
    }
}