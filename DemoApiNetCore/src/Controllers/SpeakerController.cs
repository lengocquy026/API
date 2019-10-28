using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreCodeCamp.Data;
using CoreCodeCamp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreCodeCamp.Controllers
{
    [Route("api/[controller]")]
    public class SpeakerController : Controller
    {
        private readonly ICampRepository _campRepository;
        private readonly IMapper _mapper;

        public SpeakerController(ICampRepository campRepository, IMapper mapper)
        {
            _campRepository = campRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSpeakers()
        {
            try
            {
                var SpeakerList = await _campRepository.GetAllSpeakersAsync();

                if (!SpeakerList.Any())
                {
                    return NotFound();
                }

                return Ok(SpeakerList);
            }
            catch
            {
                return this.StatusCode(500, "Database Failure");
            }
        }

        [HttpGet("{speakerId}")]
        public async Task<ActionResult<SpeakerModel>> GetSpeaker(int speakerId)
        {
            try
            {
                var Speaker = await _campRepository.GetSpeakerAsync(speakerId);

                return _mapper.Map<SpeakerModel>(Speaker);
            }
            catch
            {
                return this.StatusCode(500, "Database Failure");
            }
        }


        [HttpGet("byMoniker")]
        public async Task<ActionResult<SpeakerModel[]>> GetSpeakersByMoniker(string moniker)
        {
            try
            {
                var Speaker = await _campRepository.GetSpeakersByMonikerAsync(moniker);

                return _mapper.Map<SpeakerModel[]>(Speaker);
            }
            catch
            {
                return this.StatusCode(500, "Database Failure");
            }
        }
    }
}