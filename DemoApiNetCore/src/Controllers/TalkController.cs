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
    public class TalkController : Controller
    {
        private readonly ICampRepository _campRepository;
        private readonly IMapper _mapper;

        public TalkController(ICampRepository campRepository, IMapper mapper)
        {
            _campRepository = campRepository;
            _mapper = mapper;
        }

        [HttpGet("talk")]
        public async Task<ActionResult<TalkModel>> GetTalkByMoniker(string moniker, int talkId, bool includeSpeakers = false)
        {
            try
            {
                var Talker = await _campRepository.GetTalkByMonikerAsync(moniker, talkId, includeSpeakers);

                return Ok(_mapper.Map<TalkModel>(Talker));
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Database Failure");
            }
        }

        [HttpGet("talks")]
        public async Task<ActionResult<TalkModel[]>> GetTalksByMoniker(string moniker, bool includeSpeakers = false)
        {
            try
            {
                var Talker = await _campRepository.GetTalksByMonikerAsync(moniker, includeSpeakers);

                return Ok(_mapper.Map<TalkModel[]>(Talker));
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Database Failure");
            }
        }
    }
}