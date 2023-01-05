
using AutoMapper;
using InnoGotchi.API.Contracts;
using InnoGotchi.API.Entities.DataTransferObjects;
using InnoGotchi.API.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InnoGotchi.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;

        public UsersController (IRepositoryManager repository, IMapper mapper, ILoggerManager logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = repository.User.GetAllUsers(trackChanges: false);
            return Ok(users);
        }

        [HttpGet("{login}", Name = "GetByLogin")]
        public IActionResult GetUserByLogin([FromRoute]string login)
        {
            var user = repository.User.GetUserByLogin(login, trackChanges: false);
            if (user == null)
            {
                return NotFound("The user is not found.");
            }
            return Ok(user);
        }

        [HttpGet("{login}/info")]
        public IActionResult GetUserInfo([FromRoute]string login)
        {
            var user = repository.User.GetUserByLogin(login, trackChanges: false);
            if (user != null)
            {
                var userInfo = mapper.Map<UserInfoDto>(user);
                return Ok(userInfo);
            }
            return NotFound("The user is not found.");
        }

        [HttpPut("{login}/info")]
        public IActionResult ChangeUserInfo([FromRoute]string login, [FromBody]UserInfoDto userInfo)
        {
            var user = repository.User.GetUserByLogin(login, trackChanges: false);
            if (user != null)
            {
                user.Name = userInfo.Name;
                user.Surname = userInfo.Surname;
                user.Email = userInfo.Email;
                user.Password = userInfo.Password;
                user.Age = userInfo.Age;
                user.AvatarFileName = userInfo.AvatarFileName;
                user.IsInGame = userInfo.IsInGame;
                user.IsMusic = userInfo.IsMusic;

                repository.User.UpdateUser(user);
                repository.Save();

                return Ok("User info was sucsessfully changed.");
            }
            return BadRequest($"The user is not found.");
        }

        [HttpPut("{login}/entered")]
        public IActionResult UserEnteredHisFarm([FromRoute]string login)
        {
            DateTime now = DateTime.Now;
            var user = repository.User.GetUserByLogin(login, trackChanges: false);
            if (user != null)
            {
                user.LastEntry = now;
                repository.User.UpdateUser(user);
                repository.Save();

                return Ok();
            }
            else
            {
                return BadRequest($"The user with name \"{login}\" is not exist.");
            }
        }

        [HttpPut("{login}/left")]
        public IActionResult UserLeftHisFarm([FromRoute]string login)
        {
            DateTime now = DateTime.Now;
            var user = repository.User.GetUserByLogin(login, trackChanges: false);
            if (user != null)
            {
                user.LastExit = now;
                repository.User.UpdateUser(user);
                repository.Save();

                return Ok();
            }
            else
            {
                return BadRequest($"The user with name \"{login}\" is not exist.");
            }
        }

        [HttpPost("authorization")]
        public IActionResult AuthorizeUser([FromBody]UserForAuthorizationDto userData)
        {
            var user = repository.User.GetUserByLogin(userData.Login, trackChanges: false);
            if (user == null)
            {
                return NotFound($"The user with login \"{userData.Login}\" is not found.");
            }
            else
            {
                if (user.Password == userData.Password)
                {
                    return Ok(user);
                }
                else
                {
                    return BadRequest("Invalid password.");
                }
            }
        }

        [HttpPost("registration")]
        public IActionResult RegisterUser([FromBody]UserForRegistrationDto userData)
        {
            var sameUser = repository.User.GetUserByLogin(userData.Login, trackChanges: false);
            if (sameUser == null)
            {
                var user = mapper.Map<User>(userData);
                repository.User.CreateUserByRegData(user);
                repository.Save();

                var userToReturn = repository.User.GetUserByLogin(userData.Login, trackChanges: false);
                return CreatedAtRoute("GetByLogin", routeValues: new { login = userToReturn.Login }, value: userToReturn);
            }
            else
            {
                return BadRequest("The user with this login already exists.");
            }
        }

        [HttpDelete("remove")]
        public IActionResult DeleteUser([FromBody]int userId)
        {
            var userToDelete = repository.User.GetUserById(userId, trackChanges: false);
            if (userToDelete != null)
            {
                repository.User.DeleteUser(userToDelete);
                repository.Save();
                return Ok($"The user with ID \"{userId}\" was sucsessfuly deleted.");
            }
            else
            {
                return NotFound("The user is not found.");
            }
        }
    }
}
