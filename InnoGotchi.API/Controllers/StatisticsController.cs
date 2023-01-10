using AutoMapper;
using InnoGotchi.API.Contracts;
using InnoGotchi.API.Entities.DataTransferObjects;
using InnoGotchi.API.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.API.Controllers
{
    [ApiController]
    [Route("innogotchi/farms/{farmName}/statistics")]
    [Authorize]
    public class StatisticsController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;

        public StatisticsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetFarmStatistics([FromRoute] string farmName)
        {
            UserClaims? userClaims = (UserClaims?)HttpContext.Items["User"];
            if (farmName == userClaims.OwnFarm)
            {
                var farm = repository.Farm.GetFarmByFarmId(Convert.ToInt32(userClaims.OwnFarm), trackChanges: false);
                var statistics = repository.Statistics.GetStatisticsByFarmId(farm.Id, trackChanges: false);
                var statisticsToReturn = mapper.Map<StatisticsDto>(statistics);
                return Ok(statisticsToReturn);
            }
            return Forbid("You have no rights to get someone else's farm statistics.");
        }

        [HttpPut("update")]
        public IActionResult UpdateFarmStatistics([FromRoute] string farmName, [FromBody]StatisticsDto statisticsDto)
        {
            UserClaims? userClaims = (UserClaims?)HttpContext.Items["User"];
            if (farmName == userClaims.OwnFarm)
            {
                var farm = repository.Farm.GetFarmByFarmId(Convert.ToInt32(userClaims.OwnFarm), trackChanges: false);
                var statistics = repository.Statistics.GetStatisticsByFarmId(farm.Id, trackChanges: false);

                statistics.AlivePetsCount = statisticsDto.AlivePetsCount;
                statistics.DeadPetsCount = statisticsDto.DeadPetsCount;
                statistics.AverageFeedingPeriod = statisticsDto.AverageFeedingPeriod;
                statistics.AverageThirstPeriod = statisticsDto.AverageThirstPeriod;
                statistics.AverageHappinessPeriod = statisticsDto.AverageHappinessPeriod;
                statistics.AverageAge = statisticsDto.AverageAge;

                repository.Statistics.UpdateStatistics(statistics);
                repository.Save();

                return Ok($"Statistics of the farm \"{farmName}\" was sucsessfyly updated.");
            }
            return Forbid("You have no rights to get someone else's farm statistics.");
        }
    }
}
