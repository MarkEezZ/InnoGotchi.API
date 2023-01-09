using AutoMapper;
using InnoGotchi.API.Contracts;
using InnoGotchi.API.Entities.DataTransferObjects;
using InnoGotchi.API.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.API.Controllers
{
    [ApiController]
    [Route("api/farms/{farmName}/guests")]
    public class GuestsController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;

        public GuestsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;   
        }

        [HttpGet]
        public IActionResult GetAllFarmGuests([FromRoute]string farmName)
        {
            var farm = repository.Farm.GetFarmByFarmName(farmName, trackChanges: false);
            if (farm != null)
            {
                var guestsRecords = repository.Guests.GetGuestsByFarmId(farm.Id, trackChanges: false);
                if (guestsRecords != null)
                {
                    List<GuestInfo> guests = new List<GuestInfo>();
                    foreach (Guests guestsRecord in guestsRecords)
                    {
                        User user = repository.User.GetUserById(guestsRecord.UserId, trackChanges: false);
                        GuestInfo guest = mapper.Map<GuestInfo>(user);
                        guests.Add(guest);
                    }
                    return Ok(guests);
                }
                return Ok("There is no invited friends on this farm.");
            }
            return BadRequest($"There is no farm with name \"{farmName}\".");
        }

        [HttpGet("users")]
        public IActionResult GetAllUsersToInvite([FromRoute]string farmName)
        {
            var users = repository.User.GetAllUsers(trackChanges: false);
            if (users != null)
            {
                var farm = repository.Farm.GetFarmByFarmName(farmName, trackChanges: false);
                var farmGuestsRecords = repository.Guests.GetGuestsByFarmId(farm.Id, trackChanges: false);
                var farmOwnerRecord = repository.Owners.GetUserByOwnFarmId(farm.Id, trackChanges: false);

                List<User> usersToReturn = new List<User>();
                foreach (User user in users)
                {
                    if (farmOwnerRecord.UserId != user.Id)
                    {
                        if (farmGuestsRecords != null)
                        {
                            bool isGuest = false;
                            foreach (Guests guestsRecord in farmGuestsRecords)
                            {
                                if (guestsRecord.UserId == user.Id)
                                {
                                    isGuest = true;
                                    break;
                                }
                            }
                            if (!isGuest)
                            {
                                usersToReturn.Add(user);
                            }
                        }
                        else
                        {
                            usersToReturn.Add(user);
                        }
                    }
                }
                List<GuestInfo> usersInfoToReturn = new List<GuestInfo>();
                foreach (User user in usersToReturn)
                {
                    var userInfoToReturn = mapper.Map<GuestInfo>(user);
                    usersInfoToReturn.Add(userInfoToReturn);
                }
                return Ok(usersInfoToReturn);
            }
            return NotFound("Users was not found.");
        }

        [HttpPost("invite")]
        public IActionResult InviteGuest([FromRoute]string farmName, [FromBody]GuestInfo userInfo)
        {
            var farm = repository.Farm.GetFarmByFarmName(farmName, trackChanges: false);
            if (farm != null)
            {
                var user = repository.User.GetUserByLogin(userInfo.Login, trackChanges: false);
                var guestRecord = repository.Guests.GetGuestByUserAndFarm(user.Id, farm.Id, trackChanges: false);
                if (guestRecord == null) 
                {
                    var futureGuest = repository.User.GetUserByLogin(userInfo.Login, trackChanges: false);
                    Guests guest = new Guests();
                    guest.UserId = futureGuest.Id;
                    guest.FarmId = farm.Id;
                    repository.Guests.CreateGuest(guest);
                    repository.Save();

                    return Ok("Guest was successfuly invited.");
                }
                return BadRequest("Htis guest already invited on the farm.");
            }
            return BadRequest($"There is no farm with name \"{farmName}\".");
        }

        [HttpDelete("delete")]
        public IActionResult DeleteGuest([FromRoute]string farmName, [FromBody]GuestInfo guestInfo)
        {
            var farm = repository.Farm.GetFarmByFarmName(farmName, trackChanges: false);
            if (farm != null)
            {
                var guest = repository.User.GetUserByLogin(guestInfo.Login, trackChanges: false);
                if (guest != null)
                {
                    Guests guestRecord = repository.Guests.GetGuestByUserAndFarm(guest.Id, farm.Id, trackChanges: false);
                    repository.Guests.DeleteGuest(guestRecord);
                    repository.Save();

                    return Ok("Guest has been successfuly deleted.");
                }
                return NotFound("The guest you are trying to delete is not found.");
            }
            return BadRequest($"There is no farm with name \"{farmName}\".");
        }
    }
}
