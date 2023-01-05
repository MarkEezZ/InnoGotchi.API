using AutoMapper;
using InnoGotchi.API.Contracts;
using InnoGotchi.API.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.API.Controllers
{
    [ApiController]
    [Route("api/farms/{farmName}/statistics")]
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

        [HttpPost]
        public IActionResult GetFarmStatistics([FromRoute]string farmName)
        {
            var farm = repository.Farm.GetFarmByFarmName(farmName, trackChanges: false);
            if (farm != null)
            {
                var statistics = repository.Statistics.GetStatisticsByFarmId(farm.Id, trackChanges: false);
                var statisticsToReturn = mapper.Map<StatisticsDto>(statistics);
                return Ok(statisticsToReturn);
            }
            return BadRequest($"There is no farm with name \"{farmName}\".");
        }

        [HttpPut("update")]
        public IActionResult UpdateFarmStatistics([FromRoute]string farmName, [FromBody]StatisticsDto statisticsDto)
        {
            var farm = repository.Farm.GetFarmByFarmName(farmName, trackChanges: false);
            if (farm != null)
            {
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
            return BadRequest($"There is no farm with name \"{farmName}\".");
        }
    }
}
