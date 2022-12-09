using InnoGotchi.API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.API.Controllers
{
    [Route("api/mouthes")]
    [ApiController]
    public class MouthesController: ControllerBase
    {
        private readonly IRepositoryManager repository;
        private readonly ILoggerManager logger;

        public MouthesController(IRepositoryManager repository, ILoggerManager logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult GetMouthes()
        {
            try
            {
                var mouthes = repository.Mouth.GetAllMouthes(trackChanges: false);
                return Ok(mouthes);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong in the {nameof(GetMouthes)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
