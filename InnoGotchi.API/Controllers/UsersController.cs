
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

        public UsersController (IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = repository.User.GetAllUsers(trackChanges: false);
            return Ok(users);
        }


        [HttpGet("{login}", Name ="GetByLogin")]
        public IActionResult GetUserByLogin(string login)
        {
            var user = repository.User.GetUserByLogin(login, trackChanges: false);
            if (user == null)
            {
                return NotFound("User is not found");
            }
            return Ok(user);
        }


        [HttpGet("{login}/info")]
        public IActionResult GetUserInfo(string login)
        {
            var user = repository.User.GetUserByLogin(login, trackChanges: false);
            if (user == null)
            {
                return NotFound("User is not found");
            }

            var userInfo = mapper.Map<UserInfoDto>(user);
            return Ok(userInfo);
        }


        [HttpPut("{login}/info")]
        public IActionResult ChangeUserInfo(string login, [FromBody]UserInfoDto userInfo)
        {
            var user = repository.User.GetUserByLogin(login, trackChanges: false);
            user.Name = userInfo.Name;
            user.Surname = userInfo.Surname;
            user.Email = userInfo.Email;
            user.Password = userInfo.Password;
            user.Age = userInfo.Age;
            user.AvatarFileName = userInfo.AvatarFileName;
            user.IsInGame = userInfo.IsInGame;
            user.IsMusic = userInfo.IsMusic;
            repository.Save();

            return Ok("User sucsessfully created");
        }


        [HttpPost("authorization")]
        public IActionResult AuthorizeUser([FromBody]UserForAuthorizationDto userData)
        {
            var user = repository.User.GetUserByLogin(userData.Login, trackChanges: false);
            if (user == null)
            {
                return NotFound($"User with login \"{userData.Login}\" is not found");
            }
            else
            {
                if (user.Password == userData.Password)
                {
                    return Ok(user);
                }
                else
                {
                    return BadRequest("Invalid password");
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
                return CreatedAtRoute("GetById", routeValues: new { id = userToReturn.Id }, value: userToReturn);
            }
            else
            {
                return BadRequest("User with this login already exists");
            }
        }


        [HttpPost("removeuser")]
        public IActionResult DeleteUser([FromBody] int userId)
        {
            var userToDelete = repository.User.GetUserById(userId, trackChanges: false);
            if (userToDelete != null)
            {
                repository.User.DeleteUser(userToDelete);
                repository.Save();
                return Ok($"User with ID {userId} was sucsessfuly deleted");
            }
            else
            {
                return NotFound("User is not found");
            }
        }
    }
}
