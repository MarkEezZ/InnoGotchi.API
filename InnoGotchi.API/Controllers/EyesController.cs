using InnoGotchi.API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.API.Controllers
{
    [Route("api/eyes")]
    [ApiController]
    public class EyesController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        private readonly ILoggerManager logger;

        public EyesController(ILoggerManager _logger, IRepositoryManager _repository)
        {
            logger = _logger;
            repository = _repository;
        }

        [HttpGet]
        public IActionResult GetEyes()
        {
            try
            {
                var eyes = repository.Eyes.GetAllEyes(trackChanges: false);
                return Ok(eyes);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong in the {nameof(GetEyes)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
