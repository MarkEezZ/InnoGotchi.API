using AutoMapper;
using InnoGotchi.API.Contracts;
using InnoGotchi.API.Entities.DataTransferObjects;
using InnoGotchi.API.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.API.Controllers
{
    [ApiController]
    [Route("api/farms")]
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

        [HttpGet("own-farm")]
        public IActionResult GetUserOwnFarm([FromBody]int userId)
        {
            var ownFarmRecord = repository.Owners.GetOwnFarmByUserId(userId, trackChanges: false);
            if (ownFarmRecord != null)
            {
                var ownFarm = repository.Farm.GetFarmByFarmId(ownFarmRecord.Id, trackChanges: false);
                return Ok(ownFarm);
            }
            return NotFound("The user does not have his own farm yet.");
        }

        [HttpGet("guest-farms")]
        public IActionResult GetUserGuestFarms([FromBody]int userId)
        {
            var guestFarmRecords = repository.Guests.GetGuestFarmsByUserId(userId, trackChanges: false);
            if (guestFarmRecords != null)
            {
                List<Farm> guestFarms = new List<Farm>();
                foreach (Guests guestFarmRecord in guestFarmRecords)
                {
                    var farm = repository.Farm.GetFarmByFarmId(guestFarmRecord.Id, trackChanges: false);
                    guestFarms.Add(farm);
                }
                return Ok(guestFarms);
            }
            return NotFound("The user does not have farms he is invited at.");
        }

        [HttpPost("creation")]
        public IActionResult CreateFarm([FromBody]FarmToCreate farmData)
        {
            var ownFarm = repository.Owners.GetOwnFarmByUserId(farmData.UserId, trackChanges: false);
            if (ownFarm == null)
            {
                Farm farm = mapper.Map<Farm>(farmData);
                repository.Farm.CreateFarm(farm);
                repository.Save();

                var farmForStatistics = repository.Farm.GetFarmByFarmName(farmData.Name, trackChanges: false);
                Statistics statistics = new Statistics();
                statistics.AlivePetsCount = 0;
                statistics.DeadPetsCount = 0;
                statistics.AverageFeedingPeriod = 0;
                statistics.AverageThirstPeriod = 0;
                statistics.AverageHappinessPeriod = 0;
                statistics.AverageAge = 0;
                statistics.FarmId = farmForStatistics.Id;
                repository.Statistics.CreateStatistics(statistics);
                repository.Save();

                var farmToReturn = repository.Farm.GetFarmByFarmName(farmData.Name, trackChanges: false);
                return CreatedAtRoute("GetFarmByName", routeValues: new { name = farmToReturn.Name }, value: farmToReturn);
            }
            return Forbid("User already has a farm.");
        }
    }
}
