﻿using AutoMapper;
using InnoGotchi.API.Contracts;
using InnoGotchi.API.Entities.DataTransferObjects;
using InnoGotchi.API.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InnoGotchi.API.Controllers
{
    [Route("api/eyes")]
    [ApiController]
    public class EyesController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;

        public EyesController(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetEyes()
        {
            var eyes = repository.Eyes.GetAllEyes(trackChanges: false);
            if (eyes != null)
            {
                return Ok(eyes);
            }
            return NotFound("Eyes are not found");
        }

        [HttpPost]
        public IActionResult CreateEyes([FromBody]BodyPartDto eyesToCreate)
        {
            Console.WriteLine($"\n\n{JsonSerializer.Serialize(eyesToCreate)}\n\n");
            var sameEyes = repository.Eyes.GetEyesByName(eyesToCreate.Name, trackChanges: false);
            if (sameEyes == null)
            {
                var eyes = mapper.Map<Eyes>(eyesToCreate);
                repository.Eyes.CreateEyes(eyes);
                repository.Save();

                return Ok("Eyes was successfuly added");
            }
            return BadRequest($"Eyes with name {eyesToCreate.Name} already exists");
        }

        [HttpDelete]
        public IActionResult DeleteEyes([FromBody]BodyPartDto eyesToDelete)
        {
            var eyes = repository.Eyes.GetEyesByName(eyesToDelete.Name, trackChanges: false);
            if (eyes != null)
            {
                repository.Eyes.DeleteEyes(eyes);
                repository.Save();
                return Ok("Eyes was successfuly deleted");
            }
            return BadRequest($"There is no eyes with name {eyesToDelete.Name}");
        }
    }
}
