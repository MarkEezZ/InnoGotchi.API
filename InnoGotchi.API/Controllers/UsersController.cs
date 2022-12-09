
using InnoGotchi.API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        private readonly ILoggerManager logger;

        public UsersController (IRepositoryManager repository, ILoggerManager logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                var users = repository.User.GetAllUsers(trackChanges: false);
                return Ok(users);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong in the {nameof(GetUsers)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
