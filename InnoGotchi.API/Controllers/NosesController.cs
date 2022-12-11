using InnoGotchi.API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.API.Controllers
{
    [Route("api/noses")]
    [ApiController]
    public class NosesController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        private readonly ILoggerManager logger;

        public NosesController(ILoggerManager _logger, IRepositoryManager _repository)
        {
            logger = _logger;
            repository = _repository;
        }

        [HttpGet]
        public IActionResult GetNoses()
        {
            try
            {
                var noses = repository.Nose.GetAllNoses(trackChanges: false);
                return Ok(noses);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong in the {nameof(GetNoses)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
