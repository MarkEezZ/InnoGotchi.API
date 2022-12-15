
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


        [HttpGet("{id}", Name ="GetById")]
        public IActionResult GetUserById(int id)
        {
            var user = repository.User.GetUserById(id, trackChanges: false);
            if (user == null)
            {
                return NotFound("User is not found");
            }
            return Ok(user);
        }


        [HttpGet("{id}/info")]
        public IActionResult GetUserInfo(int id)
        {
            var user = repository.User.GetUserById(id, trackChanges: false);
            if (user == null)
            {
                return NotFound("User is not found");
            }

            var userInfo = mapper.Map<UserInfoDto>(user);
            return Ok(userInfo);
        }


        //[HttpPut("{id}/info")]
        //public IActionResult ChangeUserInfo([FromBody]UserInfoDto userInfo, int id)
        //{
        //    var settings = new Settings
        //    {

        //    }
        //    var user = repository.User.GetUserById(id, trackChanges: false);
        //    var updatedUser = mapper.Map<User>(userInfo);
        //}


        [HttpPost("authorization")]
        public IActionResult AuthorizeUser([FromBody]UserForAuthorizationDto userData)
        {
            Console.WriteLine(userData.Login + userData.Password);
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
