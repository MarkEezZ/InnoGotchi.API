using InnoGotchi.API.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using InnoGotchi.API.Contracts;
using InnoGotchi.API.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InnoGotchi.API.Controllers
{
    [ApiController]
    [Route("innogotchi/farms")]
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

        [HttpGet("{farmName}/pets")]
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
                    bool isDead = checkIsDead(pet);
                    pet.isDead = isDead;
                    repository.Pet.UpdatePet(pet);
                    repository.Save();

                    PetToReturnDto petToReturn = ConvertToDto(pet);

                    petsToReturn.Add(petToReturn);
                }
                return Ok(petsToReturn);
            }
            return Ok();
        }

        [HttpGet("pets")]
        [Authorize(Policy = "Admin")]
        public IActionResult GetAllPets()
        {
            var pets = repository.Pet.GetAllPets(trackChanges: false);

            if (pets != null)
            {
                List<PetToReturnDto> petsToReturn = new List<PetToReturnDto>();
                foreach (Pet pet in pets)
                {
                    bool isDead = checkIsDead(pet);
                    pet.isDead = isDead;
                    repository.Pet.UpdatePet(pet);
                    repository.Save();

                    PetToReturnDto petToReturn = ConvertToDto(pet);

                    petsToReturn.Add(petToReturn);
                }
                return Ok(petsToReturn);
            }
            return Ok();
        }

        [HttpGet("pets/{petName}", Name = "GetPetByName")]
        [Authorize]
        public IActionResult GetPetByName([FromRoute] string petName)
        {
            var pet = repository.Pet.GetPetByName(petName, trackChanges: false);
            if (pet != null)
            {
                bool isDead = checkIsDead(pet);
                pet.isDead = isDead;
                repository.Pet.UpdatePet(pet);
                repository.Save();

                PetToReturnDto petToReturn = ConvertToDto(pet);

                return Ok(petToReturn);
            }
            return NotFound($"Pet with name \"{petName}\" is not found.");
        }

        [HttpPatch("{farmName}/pets/{petName}/resurrect")]
        [Authorize]
        public IActionResult ResurrectPet([FromRoute] string farmName, [FromRoute] string petName)
        {
            var pet = repository.Pet.GetPetByName(petName, trackChanges: false);
            if (pet != null)
            {
                var farm = repository.Farm.GetFarmByFarmName(farmName, trackChanges: false);
                if (farm != null)
                {
                    if (pet.FarmId == farm.Id)
                    {
                        pet.LastDrinkTime = DateTime.Now;
                        pet.LastEatTime = DateTime.Now;
                        pet.LastHealthTime = DateTime.Now;
                        pet.LastMoodTime = DateTime.Now;
                        pet.isDead = false;

                        repository.Pet.UpdatePet(pet);
                        repository.Save();

                        PetToReturnDto petToReturn = ConvertToDto(pet);

                        return Ok(petToReturn);
                    }
                    return BadRequest("I don't now how did you resurrected a pet on the farm where are you not at.");
                }
                return NotFound($"Farm with name \"{farmName}\" is not found.");
            }
            return NotFound($"Pet with name \"{petName}\" is not found.");
        }

        [HttpPatch("{farmName}/pets/{petName}/dead")]
        [Authorize]
        public IActionResult PetDead([FromRoute] string farmName, [FromRoute] string petName)
        {
            var pet = repository.Pet.GetPetByName(petName, trackChanges: false);
            if (pet != null)
            {
                var farm = repository.Farm.GetFarmByFarmName(farmName, trackChanges: false);
                if (farm != null)
                {
                    if (pet.FarmId == farm.Id)
                    {
                        pet.isDead = true;

                        repository.Pet.UpdatePet(pet);
                        repository.Save();

                        PetToReturnDto petToReturn = ConvertToDto(pet);

                        return Ok(petToReturn);
                    }
                    return BadRequest("I don't now how did you killed a pet on the farm where are you not at.");
                }
                return NotFound($"Farm with name \"{farmName}\" is not found.");
            }
            return NotFound($"Pet with name \"{petName}\" is not found.");
        }

        [HttpPatch("pets/{petName}/feed")]
        [Authorize]
        public IActionResult FeedPet([FromRoute] string petName)
        {
            var pet = repository.Pet.GetPetByName(petName, trackChanges: false);
            if (pet != null)
            {
                pet.LastEatTime = DateTime.Now;
                repository.Pet.UpdatePet(pet);
                repository.Save();

                PetToReturnDto petToReturn = ConvertToDto(pet);

                return Ok(petToReturn);
            }
            return NotFound($"Pet with name \"{petName}\" is not found.");
        }

        [HttpPatch("pets/{petName}/water")]
        [Authorize]
        public IActionResult WaterPet([FromRoute] string petName)
        {
            Console.WriteLine("\n\n###\n\n");
            var pet = repository.Pet.GetPetByName(petName, trackChanges: false);
            if (pet != null)
            {
                pet.LastDrinkTime = DateTime.Now;
                repository.Pet.UpdatePet(pet);
                repository.Save();

                PetToReturnDto petToReturn = ConvertToDto(pet);

                return Ok(petToReturn);
            }
            return NotFound($"Pet with name \"{petName}\" is not found.");
        }

        [HttpPatch("pets/{petName}/cure")]
        [Authorize]
        public IActionResult CurePet([FromRoute] string petName)
        {
            var pet = repository.Pet.GetPetByName(petName, trackChanges: false);
            if (pet != null)
            {
                pet.LastHealthTime = DateTime.Now;
                pet.isDead = false;
                repository.Pet.UpdatePet(pet);
                repository.Save();

                PetToReturnDto petToReturn = ConvertToDto(pet);

                return Ok(petToReturn);
            }
            return NotFound($"Pet with name \"{petName}\" is not found.");
        }

        [HttpPatch("pets/{petName}/cheer-up")]
        [Authorize]
        public IActionResult CheerUpPet([FromRoute] string petName)
        {
            var pet = repository.Pet.GetPetByName(petName, trackChanges: false);
            if (pet != null)
            {
                pet.LastMoodTime = DateTime.Now;
                repository.Pet.UpdatePet(pet);
                repository.Save();

                PetToReturnDto petToReturn = ConvertToDto(pet);

                return Ok(petToReturn);
            }
            return NotFound($"Pet with name \"{petName}\" is not found.");
        }

        [HttpPatch("{farmName}/pets/{petName}/moved")]
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

        [HttpPost("{farmName}/pets/constructor")]
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
                    pet.FarmId = farm.Id;

                    repository.Pet.CreatePet(pet);
                    repository.Save();

                    var petFromDb = repository.Pet.GetPetByName(petData.Name, trackChanges: false);

                    PetToReturnDto petToReturn = ConvertToDto(petFromDb);

                    return Ok(petToReturn);
                }
                return BadRequest("This name is already taken.");
            }
            return BadRequest("You have no rights to create a pet on someone else's farm");
        }

        [HttpDelete("{farmName}/pets/delete")]
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

        private PetToReturnDto ConvertToDto(Pet pet)
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

            return petToReturn;
        }

        private bool checkIsDead(Pet pet)
        {
            long dayInMilliseconds = 86400000;
            long periodForEat = dayInMilliseconds * 1;
            long periodForDrink = dayInMilliseconds * 1;
            long periodForHealth = dayInMilliseconds * 1;
            long periodForMood = dayInMilliseconds * 1;

            double foodL = Math.Round(100 - (DateTime.Now - pet.LastEatTime).TotalMilliseconds / periodForEat * 100);
            double drinkL = Math.Round(100 - (DateTime.Now - pet.LastDrinkTime).TotalMilliseconds / periodForDrink * 100);
            double moodL = Math.Round(100 - (DateTime.Now - pet.LastMoodTime).TotalMilliseconds / periodForMood * 100);
            double healthL = Math.Round(100 - (DateTime.Now - pet.LastHealthTime).TotalMilliseconds / periodForHealth * 100);

            if (healthL <= 0)
            {
                return true;
            }
            return false;
        }
    }
}