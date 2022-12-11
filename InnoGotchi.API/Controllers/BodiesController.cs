using InnoGotchi.API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.API.Controllers
{
    [Route("api/bodies")]
    [ApiController]
    public class BodiesController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        private readonly ILoggerManager logger;

        public BodiesController(IRepositoryManager _repository, ILoggerManager _logger)
        {
            repository = _repository;
            logger = _logger;
        }

        [HttpGet]
        public IActionResult GetBodies()
        {
            try
            {
                var bodies = repository.Body.GetAllBodies(trackChanges: false);
                return Ok(bodies);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong in the {nameof(GetBodies)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
