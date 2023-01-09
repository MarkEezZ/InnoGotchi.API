
using AutoMapper;
using InnoGotchi.API.Contracts;
using InnoGotchi.API.Entities.DataTransferObjects;
using InnoGotchi.API.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace InnoGotchi.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public UsersController (IRepositoryManager repository, IMapper mapper, ILoggerManager logger, IConfiguration configuration)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
            this.configuration = configuration;
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
                user.PasswordHash = createPasswordHash(userInfo.Password);
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
            return BadRequest($"The user with name \"{login}\" is not exist.");
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
            return BadRequest($"The user with name \"{login}\" is not exist.");
        }

        [HttpDelete("remove")]
        public IActionResult DeleteUser([FromBody]Guid userId)
        {
            var userToDelete = repository.User.GetUserById(userId, trackChanges: false);
            string userName = userToDelete.Login;

            if (userToDelete != null)
            {
                Owners ownerRecord = repository.Owners.GetOwnFarmByUserId(userToDelete.Id, trackChanges: false);
                if (ownerRecord != null)
                {
                    var farm = repository.Farm.GetFarmByFarmId(ownerRecord.FarmId, trackChanges: false);
                    repository.Farm.DeleteFarm(farm);
                    repository.Owners.RemoveFarmOwner(ownerRecord);
                }

                repository.User.DeleteUser(userToDelete);
                repository.Save();
                return Ok($"The user \"{userName}\" was sucsessfuly deleted.");
            }
            return NotFound("The user is not found.");
        }

        [HttpPost("registration")]
        public IActionResult RegisterUser([FromBody]UserForRegistrationDto userData)
        {
            var sameUser = repository.User.GetUserByLogin(userData.Login, trackChanges: false);
            if (sameUser == null)
            {
                User user = mapper.Map<User>(userData);
                user.PasswordHash = createPasswordHash(userData.Password);
                repository.User.CreateUser(user);
                repository.Save();

                var userToReturn = repository.User.GetUserByLogin(userData.Login, trackChanges: false);
                return CreatedAtRoute("GetByLogin", routeValues: new { login = userToReturn.Login }, value: userToReturn);
            }
            return BadRequest("The user with this login already exists.");
        }

        [HttpPost("authorization")]
        public IActionResult AuthorizeUser([FromBody] UserForAuthorizationDto userData)
        {
            var user = repository.User.GetUserByLogin(userData.Login, trackChanges: false);
            if (user == null)
            {
                return NotFound($"The user with login \"{userData.Login}\" is not found.");
            }
            else
            {
                string passwordHash = createPasswordHash(userData.Password);

                if (user.PasswordHash == passwordHash)
                {
                    string token = createToken(user);
                    return Ok(token);
                }
                return BadRequest("Invalid password.");
            }
        }

        private string createToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(type: "Id", user.Id.ToString()),
                new Claim(type: "Login", user.Login)
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Secret").Value));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken 
            (
                claims: claims, 
                expires: DateTime.Now.AddDays(1), 
                signingCredentials: credentials
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private string createPasswordHash(string password)
        {
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
            using (var hmacsha512 = new HMACSHA512(passwordBytes))
            {
                byte[] passwordHash = hmacsha512.ComputeHash(passwordBytes);
                return Convert.ToBase64String(passwordHash);
            }
        }
    }
}
