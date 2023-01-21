using AutoMapper;
using InnoGotchi.API.Contracts;
using InnoGotchi.API.Entities.DataTransferObjects;
using InnoGotchi.API.Entities.Models;
using InnoGotchi.API.Entities.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InnoGotchi.API.Controllers
{
    [ApiController]
    [Route("innogotchi/farms")]
    [Authorize]
    public class FarmsController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public FarmsController(IRepositoryManager repository, IMapper mapper, ILoggerManager logger, IConfiguration configuration)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
            this.configuration = configuration;
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult GetAllFarms()
        {
            var farms = repository.Farm.GetAllFarms(trackChanges: false);
            if (farms != null)
            {
                return Ok(farms);
            }
            return Ok();
        }

        [HttpGet("own-farm")]
        public IActionResult GetUserOwnFarm()
        {
            UserClaims? userClaims = (UserClaims?)HttpContext.Items["User"];
            var ownFarmRecord = repository.Owners.GetOwnFarmByUserId(userClaims!.Id, trackChanges: false);

            if (ownFarmRecord != null)
            {
                var ownFarm = repository.Farm.GetFarmByFarmId(ownFarmRecord.FarmId, trackChanges: false);

                FarmRecordDto record = new FarmRecordDto();
                record.FarmName = ownFarm.Name;
                record.FarmOwnerLogin = userClaims.Login;

                return Ok(record);
            }
            return NotFound("The user does not have his own farm yet.");
        }

        [HttpGet("guest-farms")]
        public IActionResult GetUserGuestFarms()
        {
            UserClaims? userClaims = (UserClaims?)HttpContext.Items["User"];
            var guestFarmRecords = repository.Guests.GetGuestFarmsByUserId(userClaims!.Id, trackChanges: false);

            if (!guestFarmRecords.IsNullOrEmpty())
            {
                List<FarmRecordDto> guestFarms = new List<FarmRecordDto>();
                foreach (Guests guestFarmRecord in guestFarmRecords)
                {
                    var farm = repository.Farm.GetFarmByFarmId(guestFarmRecord.FarmId, trackChanges: false);
                    var farmOwnerRecord = repository.Owners.GetUserByOwnFarmId(farm.Id, trackChanges: false);
                    var owner = repository.User.GetUserById(farmOwnerRecord.UserId, trackChanges: false);

                    FarmRecordDto record = new FarmRecordDto();
                    record.FarmName = farm.Name;
                    record.FarmOwnerLogin = owner.Login;
                    guestFarms.Add(record);
                }
                return Ok(guestFarms);
            }
            return NotFound("The user does not have farms he is invited at.");
        }

        [HttpPost("create")]
        public IActionResult CreateFarm([FromBody] FarmToCreate farmData)
        {
            UserClaims? userClaims = (UserClaims?)HttpContext.Items["User"];
            var ownFarm = repository.Owners.GetOwnFarmByUserId(userClaims!.Id, trackChanges: false);

            if (ownFarm == null)
            {
                Farm farm = mapper.Map<Farm>(farmData);
                repository.Farm.CreateFarm(farm);
                repository.Save();

                var farmForStatistics = repository.Farm.GetFarmByFarmName(farmData.Name, trackChanges: false);

                StatisticsBase baseStatistics = new StatisticsBase();
                Statistics statistics = mapper.Map<Statistics>(baseStatistics);
                statistics.FarmId = farmForStatistics.Id;
                repository.Statistics.CreateStatistics(statistics);
                repository.Save();

                Owners owner = new Owners();
                owner.UserId = userClaims.Id;
                owner.FarmId = farmForStatistics.Id;
                repository.Owners.AddFarmOwner(owner);
                repository.Save();

                User user = repository.User.GetUserById(userClaims!.Id, trackChanges: false);
                createToken(user);

                return Ok($"Farm \"{farmData.Name}\" was successfuly created.");
            }
            return BadRequest("User already has a farm.");
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
                MaxAge = TimeSpan.FromMinutes(AuthOptions.LIFETIME)
            });

            return jwtToken;
        }
    }
}
