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

        [HttpGet]
        public IActionResult GetAllFarms()
        {
            var farms = repository.Farm.GetAllFarms(trackChanges: false);
            if (farms != null)
            {
                return Ok(farms);
            }
            return BadRequest("Farms are not found");
        }

        [HttpPost("own-farm")]
        public IActionResult GetUserOwnFarm([FromBody] Guid userId)
        {
            var ownFarmRecord = repository.Owners.GetOwnFarmByUserId(userId, trackChanges: false);
            if (ownFarmRecord != null)
            {
                var ownFarm = repository.Farm.GetFarmByFarmId(ownFarmRecord.Id, trackChanges: false);
                return Ok(ownFarm);
            }
            return NotFound("The user does not have his own farm yet.");
        }

        [HttpPost("guest-farms")]
        public IActionResult GetUserGuestFarms([FromBody] Guid userId)
        {
            var guestFarmRecords = repository.Guests.GetGuestFarmsByUserId(userId, trackChanges: false);
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

                Owners owner = new Owners();
                owner.UserId = farmData.UserId;
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
