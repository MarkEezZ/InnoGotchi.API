using AutoMapper;
using InnoGotchi.API.Contracts;
using InnoGotchi.API.Entities.DataTransferObjects;
using InnoGotchi.API.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;

namespace InnoGotchi.API.Controllers
{
    [Route("innogotchi/bodies")]
    [ApiController]
    [Authorize]
    public class BodiesController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;

        public BodiesController(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllBodies()
        {
            var bodies = repository.Body.GetAllBodies(trackChanges: false);
            if (bodies != null)
            {
                return Ok(bodies);
            }
            return NotFound("Bodies are not found.");
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public IActionResult CreateBody([FromBody] BodyDto bodyToCreate)
        {
            var sameBody = repository.Body.GetBodyByName(bodyToCreate.Name, trackChanges: false);
            if (sameBody == null)
            {
                var body = mapper.Map<Body>(bodyToCreate);
                repository.Body.CreateBody(body);
                repository.Save();

                return Ok("Body was successfuly created.");
            }
            return BadRequest($"Body with name \"{bodyToCreate.Name}\" already exists.");
        }

        [HttpDelete]
        [Authorize(Policy = "Admin")]
        public IActionResult DeleteBody([FromBody] BodyDto bodyToDelete)
        {
            var body = repository.Body.GetBodyByName(bodyToDelete.Name, trackChanges: false);
            if (body != null)
            {
                repository.Body.DeleteBody(body);
                repository.Save();
                return Ok("Body was successfuly deleted.");
            }
            return BadRequest($"There is no body with name \"{bodyToDelete.Name}\".");
        }
    }
}
