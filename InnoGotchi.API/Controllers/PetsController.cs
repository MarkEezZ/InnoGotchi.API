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
                List<PetToReturnDto> petsToReturn = new List<PetToReturnDto>();
                foreach (Pet pet in pets)
                {
                    PetToReturnDto petToReturn = mapper.Map<PetToReturnDto>(pet);

                    Body body = repository.Body.GetBodyById(pet.BodyId, trackChanges: false);
                    petToReturn.Body = mapper.Map<BodyDto>(body);

                    Eyes eyes = repository.Eyes.GetEyesById(pet.EyesId, trackChanges: false);
                    petToReturn.Eyes = mapper.Map<BodyPartDto>(eyes);

                    if (pet.NoseId != null)
                    {
                        Nose nose = repository.Nose.GetNoseById((int)pet.NoseId, trackChanges: false);
                        petToReturn.Nose = mapper.Map<BodyPartDto>(nose);
                    }

                    Mouth mouth = repository.Mouth.GetMouthById(pet.MouthId, trackChanges: false);
                    petToReturn.Mouth = mapper.Map<BodyPartDto>(mouth);

                    petsToReturn.Add(petToReturn);
                }
                return Ok(petsToReturn);
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
                PetToReturnDto petToReturn = mapper.Map<PetToReturnDto>(pet);

                Body body = repository.Body.GetBodyById(pet.BodyId, trackChanges: false);
                petToReturn.Body = mapper.Map<BodyDto>(body);

                Eyes eyes = repository.Eyes.GetEyesById(pet.EyesId, trackChanges: false);
                petToReturn.Eyes = mapper.Map<BodyPartDto>(eyes);

                if (pet.NoseId != null)
                {
                    Nose nose = repository.Nose.GetNoseById((int)pet.NoseId, trackChanges: false);
                    petToReturn.Nose = mapper.Map<BodyPartDto>(nose);
                }

                Mouth mouth = repository.Mouth.GetMouthById(pet.MouthId, trackChanges: false);
                petToReturn.Mouth = mapper.Map<BodyPartDto>(mouth);

                return Ok(petToReturn);
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

        [HttpPatch("{petName}/moved")]
        [Authorize]
        public IActionResult ChangeCoordinates([FromRoute] string petName, [FromBody] CoordinatesDto coordinates)
        {
            var pet = repository.Pet.GetPetByName(petName, trackChanges: false);
            if (pet != null)
            {
                pet.positionX = coordinates.positionX;
                pet.positionY = coordinates.positionY;
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
            var farm = repository.Farm.GetFarmByFarmName(farmName, trackChanges: false);

            if (farm.Id == Convert.ToInt32(userClaims!.OwnFarm))
            {
                var samePet = repository.Pet.GetPetByName(petData.Name, trackChanges: false);
                if (samePet == null)
                {
                    Pet pet = mapper.Map<Pet>(petData);
                    DateTime now = DateTime.Now;

                    pet.TimeOfCreating = now;
                    pet.LastDrinkTime = now;
                    pet.LastEatTime = now;
                    pet.LastHealthTime = now;
                    pet.LastMoodTime = now;
                    pet.FarmId = farm.Id;

                    repository.Pet.CreatePet(pet);
                    repository.Save();

                    var petFromDb = repository.Pet.GetPetByName(petData.Name, trackChanges: false);
                    return Ok(petFromDb);
                }
                return BadRequest("This name is already taken.");
            }
            return BadRequest("You have no rights to create a pet on someone else's farm");
        }

        [HttpDelete("delete")]
        [Authorize]
        public IActionResult DeletePet([FromRoute] string farmName, [FromBody] PetToReturnDto petData)
        {
            UserClaims? userClaims = (UserClaims?)HttpContext.Items["User"];
            var farm = repository.Farm.GetFarmByFarmName(farmName, trackChanges: false);

            if (farm.Id == Convert.ToInt32(userClaims!.OwnFarm))
            {
                var pet = repository.Pet.GetPetByName(petData.Name, trackChanges: false);
                repository.Pet.DeletePet(pet);
                repository.Save();

                return Ok("Pet was successfuly deleted.");
            }
            return BadRequest("You have no rights to delete a pet from someone else's farm.");
        }
    }
}