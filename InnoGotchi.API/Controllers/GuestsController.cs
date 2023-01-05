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

        [HttpPost]
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

        [HttpPost("invite")]
        public IActionResult InviteGuest([FromRoute]string farmName, [FromBody]int userId)
        {
            var farm = repository.Farm.GetFarmByFarmName(farmName, trackChanges: false);
            if (farm != null)
            {
                var owner = repository.Owners.GetOwnFarmByUserId(userId, trackChanges: false);
                if (owner.FarmId == farm.Id)
                {
                    Guests guest = new Guests();
                    guest.UserId = userId;
                    guest.FarmId = farm.Id;
                    repository.Guests.CreateGuest(guest);
                    repository.Save();

                    return Ok("Guest has been successfuly invited.");
                }
                return Forbid("You don't have enough rights to invite guests to someone else's farm.");
            }
            return BadRequest($"There is no farm with name \"{farmName}\".");
        }

        [HttpDelete("delete")]
        public IActionResult DeleteGuest([FromRoute]string farmName, [FromBody]GuestInfo guestInfo)
        {
            var farm = repository.Farm.GetFarmByFarmName(farmName, trackChanges: false);
            if (farm != null)
            {
                var user = repository.User.GetUserByLogin(guestInfo.Name, trackChanges: false);
                if (user != null)
                {
                    var owner = repository.Owners.GetOwnFarmByUserId(user.Id, trackChanges: false);
                    if (owner.FarmId == farm.Id)
                    {
                        Guests guest = repository.Guests.GetGuestByUserAndFarm(user.Id, farm.Id, trackChanges: false);
                        repository.Guests.DeleteGuest(guest);
                        repository.Save();

                        return Ok("Guest has been successfuly deleted.");
                    }
                    return Forbid("You don't have enough rights to invite guests to someone else's farm.");
                }
                return NotFound("The guest you are trying to delete is not found.");
            }
            return BadRequest($"There is no farm with name \"{farmName}\".");
        }
    }
}
