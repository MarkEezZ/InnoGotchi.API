using AutoMapper;
using InnoGotchi.API.Contracts;
using InnoGotchi.API.Entities.DataTransferObjects;
using InnoGotchi.API.Entities.Models;
using InnoGotchi.API.Entities.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.API.Controllers
{
    [ApiController]
    [Route("innogotchi/farms")]
    [Authorize]
    public class FarmsController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;

        public FarmsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult GetAllFarms()
        {
            var farms = repository.Farm.GetAllFarms(trackChanges: false);
            if (farms != null)
            {
                return Ok(farms);
            }
            return Ok();
        }

        [HttpGet("own-farm")]
        public IActionResult GetUserOwnFarm()
        {
            UserClaims? userClaims = (UserClaims?)HttpContext.Items["User"];
            var ownFarmRecord = repository.Owners.GetOwnFarmByUserId(userClaims.Id, trackChanges: false);

            if (ownFarmRecord != null)
            {
                var ownFarm = repository.Farm.GetFarmByFarmId(ownFarmRecord.FarmId, trackChanges: false);
                return Ok(ownFarm);
            }
            return NotFound("The user does not have his own farm yet.");
        }

        [HttpGet("guest-farms")]
        public IActionResult GetUserGuestFarms()
        {
            UserClaims? userClaims = (UserClaims?)HttpContext.Items["User"];
            var guestFarmRecords = repository.Guests.GetGuestFarmsByUserId(userClaims.Id, trackChanges: false);

            if (guestFarmRecords != null)
            {
                List<Farm> guestFarms = new List<Farm>();
                foreach (Guests guestFarmRecord in guestFarmRecords)
                {
                    var farm = repository.Farm.GetFarmByFarmId(guestFarmRecord.FarmId, trackChanges: false);
                    guestFarms.Add(farm);
                }
                return Ok(guestFarms);
            }
            return NotFound("The user does not have farms he is invited at.");
        }

        [HttpPost("create")]
        public IActionResult CreateFarm([FromBody] FarmToCreate farmData)
        {
            UserClaims? userClaims = (UserClaims?)HttpContext.Items["User"];
            var ownFarm = repository.Owners.GetOwnFarmByUserId(userClaims.Id, trackChanges: false);
            if (ownFarm == null)
            {
                Farm farm = mapper.Map<Farm>(farmData);
                repository.Farm.CreateFarm(farm);
                repository.Save();

                var farmForStatistics = repository.Farm.GetFarmByFarmName(farmData.Name, trackChanges: false);

                StatisticsBase baseStatistics = new StatisticsBase();
                Statistics statistics = mapper.Map<Statistics>(baseStatistics);
                statistics.FarmId = farmForStatistics.Id;
                repository.Statistics.CreateStatistics(statistics);
                repository.Save();

                Owners owner = new Owners();
                owner.UserId = userClaims.Id;
                owner.FarmId = farmForStatistics.Id;
                repository.Owners.AddFarmOwner(owner);
                repository.Save();

                var farmToReturn = repository.Farm.GetFarmByFarmName(farmData.Name, trackChanges: false);
                return CreatedAtRoute("GetFarmByName", routeValues: new { name = farmToReturn.Name }, value: farmToReturn);
            }
            return Forbid("User already has a farm.");
        }
    }
}
