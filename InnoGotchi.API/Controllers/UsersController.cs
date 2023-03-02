
using AutoMapper;
using InnoGotchi.API.Contracts;
using InnoGotchi.API.Entities.DataTransferObjects;
using InnoGotchi.API.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace InnoGotchi.API.Controllers
{
    [Route("innogotchi/users")]
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

        [HttpPatch("appoint-admin")]
        [Authorize(Policy = "Admin")]
        public IActionResult AppointAdmin([FromBody] Guid userId)
        {
            var user = repository.User.GetUserById(userId, trackChanges: false);
            if (user != null)
            {
                if (user.Role == Roles.ADMIN)
                {
                    user.Role = Roles.ADMIN;
                    repository.User.UpdateUser(user);
                    repository.Save();
                    return Ok($"Role of user \"{user.Login}\" was successfuly changed to \"{user.Role}\".");
                }
                return BadRequest($"User \"{user.Login}\" is already an admin");
            }
            return NotFound("User is not found.");
        }

        [HttpPatch("appoint-user")]
        [Authorize(Policy = "Admin")]
        public IActionResult AppointUser([FromBody] Guid userId)
        {
            var user = repository.User.GetUserById(userId, trackChanges: false);
            if (user != null)
            {
                if (user.Role == Roles.USER)
                {
                    user.Role = Roles.USER;
                    repository.User.UpdateUser(user);
                    repository.Save();
                    return Ok($"Role of user \"{user.Login}\" was successfuly changed to \"{user.Role}\".");
                }
                return BadRequest($"User \"{user.Login}\" is already just a user");
            }
            return NotFound("User is not found.");
        }

        [HttpGet("isautorized")]
        public IActionResult checkAutorization()
        {
            UserClaims? userClaims = (UserClaims?)HttpContext.Items["User"];
            if (userClaims != null)
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult GetAllUsers()
        {
            var users = repository.User.GetAllUsers(trackChanges: false);
            return Ok(users);
        }

        [HttpGet("{login}", Name = "GetByLogin")]
        [Authorize(Policy = "Admin")]
        public IActionResult GetUserByLogin([FromRoute] string login)
        {
            var user = repository.User.GetUserByLogin(login, trackChanges: false);
            if (user != null)
            {
                var userInfo = mapper.Map<UserInfoWithoutPasswordDto>(user);
                return Ok(userInfo);
            }
            return NotFound("User is not found.");
        }

        [HttpGet("user-info")]
        [Authorize]
        public IActionResult GetUserInfo()
        {
            UserClaims? userClaims = (UserClaims?)HttpContext.Items["User"];
            var user = repository.User.GetUserById(userClaims!.Id, trackChanges: false);
            if (user != null)
            {
                var userInfo = mapper.Map<UserInfoWithoutPasswordDto>(user);
                return Ok(userInfo);
            }
            return NotFound("User is not found.");
        }

        [HttpPut("user-info")]
        [Authorize]
        public IActionResult ChangeUserInfo([FromBody] UserInfoDto userInfo)
        {
            UserClaims? userClaims = (UserClaims?)HttpContext.Items["User"];
            var user = repository.User.GetUserById(userClaims!.Id, trackChanges: false);

            if (userInfo.Password != null && userInfo.NewPassword != null)
            {
                if (user.PasswordHash == createPasswordHash(userInfo.Password))
                {
                    user.PasswordHash = createPasswordHash(userInfo.NewPassword);
                }
                return BadRequest("Old password is incorrect.");
            }
            user.Name = userInfo.Name;
            user.Surname = userInfo.Surname;
            user.Email = userInfo.Email;
            user.Age = userInfo.Age;
            user.AvatarFileName = userInfo.AvatarFileName;
            user.IsInGame = userInfo.IsInGame;
            user.IsMusic = userInfo.IsMusic;

            repository.User.UpdateUser(user);
            repository.Save();
            return Ok("User info was sucsessfully changed.");
        }

        [HttpPut("log-out")]
        [Authorize]
        public IActionResult UserLeftHisFarm()
        {
            if (Request.Cookies["AspNetCore.Application.Id"] != null)
            {
                HttpContext.Response.Cookies.Append("AspNetCore.Application.Id", "expired",
                new CookieOptions
                {
                    MaxAge = TimeSpan.FromMinutes(0)
                });
            }
            return Ok("User was successfuly logged out.");
        }

        [HttpDelete("delete")]
        [Authorize]
        public IActionResult DeleteUser()
        {
            UserClaims? userClaims = (UserClaims?)HttpContext.Items["User"];
            var userToDelete = repository.User.GetUserById(userClaims!.Id, trackChanges: false);

            Owners ownerRecord = repository.Owners.GetOwnFarmByUserId(userToDelete.Id, trackChanges: false);
            if (ownerRecord != null)
            {
                var farm = repository.Farm.GetFarmByFarmId(ownerRecord.FarmId, trackChanges: false);
                repository.Farm.DeleteFarm(farm);
                repository.Owners.RemoveFarmOwner(ownerRecord);
            }
            repository.User.DeleteUser(userToDelete);
            repository.Save();

            return Ok($"Account was sucsessfuly deleted.");
        }

        [HttpDelete("deletebyid")]
        [Authorize(Policy = "Admin")]
        public IActionResult DeleteUserById([FromBody] Guid userId)
        {
            var userToDelete = repository.User.GetUserById(userId, trackChanges: false);
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

                return Ok($"Account was sucsessfuly deleted.");
            }
            return NotFound($"User with id \"{userId}\" is not found");
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] UserForRegistrationDto userData)
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
            return BadRequest($"User with login \"{userData.Login}\" already exists.");
        }

        [HttpPost("log-in")]
        public IActionResult AuthorizeUser([FromBody] UserForAuthorizationDto userData)
        {
            var user = repository.User.GetUserByLogin(userData.Login, trackChanges: false);
            if (user != null)
            {
                string passwordHash = createPasswordHash(userData.Password);

                if (user.PasswordHash == passwordHash)
                {
                    createToken(user);

                    return Ok("Authorized.");
                }
                return BadRequest("Invalid password.");                
            }
            return NotFound($"User with login \"{userData.Login}\" is not found.");
        }

        private string createToken(User user)
        {
            List<Claim> claims;
            var ownFarmRecord = repository.Owners.GetOwnFarmByUserId(user.Id, trackChanges: false);

            if (ownFarmRecord == null)
            {
                claims = new List<Claim>
                {
                    new Claim(type: "Id", user.Id.ToString()),
                    new Claim(type: "Login", user.Login),
                    new Claim(type: "Role", user.Role),
                    new Claim(type: "OwnFarm", "")
                };
            }
            else
            {
                var userOwnFarm = repository.Farm.GetFarmByFarmId(ownFarmRecord.FarmId, trackChanges: false);
                claims = new List<Claim>
                {
                    new Claim(type: "Id", user.Id.ToString()),
                    new Claim(type: "Login", user.Login),
                    new Claim(type: "Role", user.Role),
                    new Claim(type: "OwnFarm", userOwnFarm.Id.ToString())
                };
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Secret").Value));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken 
            (
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: DateTime.Now,
                claims: claims, 
                expires: DateTime.Now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)), 
                signingCredentials: credentials
            );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            HttpContext.Response.Cookies.Append("AspNetCore.Application.Id", jwtToken,
            new CookieOptions
            {
                MaxAge = TimeSpan.FromMinutes(AuthOptions.LIFETIME),
                Path = "/"
            });

            return jwtToken;
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
