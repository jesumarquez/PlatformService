using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repo;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Platform>> GetPlatforms()
        {
            Console.WriteLine("--> Getting Platforms...");

            var platforms = _repo.GetAllPlatforms();
            var platformsMapped = _mapper.Map<IEnumerable<PlatformReadDto>>(platforms);

            return Ok(platformsMapped);
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platformItem = _repo.GetById(id);
            if(platformItem != null)
            {
                var platformMapped = _mapper.Map<PlatformReadDto>(platformItem);

                return Ok(platformMapped);
            }
                
            return NotFound();
        }

        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform([FromBody]Platform platformCreateDto)
        {
            var platformModel = _mapper.Map<Platform>(platformCreateDto);
            _repo.CreatePlatform(platformModel);
            _repo.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

            return CreatedAtRoute(nameof(GetPlatformById), 
                new { Id = platformReadDto.Id}, 
                platformReadDto);
        }
    }
}