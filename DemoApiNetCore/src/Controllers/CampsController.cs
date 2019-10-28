using CoreCodeCamp.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using AutoMapper;
using CoreCodeCamp.Models;

namespace CoreCodeCamp.Controllers
{
    [Route("api/[controller]")]
    public class CampsController: ControllerBase
    {
        private readonly ICampRepository _campRepository;
        private readonly IMapper _mapper;

        public CampsController(ICampRepository campRepository, IMapper mapper)
        {
            _campRepository = campRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<CampModels[]>> GetCamps(bool includeTalks = true)
        {
            try
            {
                var listCamps = await _campRepository.GetAllCampsAsync(includeTalks);
                CampModels[] campModels = _mapper.Map<CampModels[]>(listCamps);

                return campModels;
            }
            catch(Exception)
            {
                return this.StatusCode(500, "Database Failure");
            }
        }

        [HttpGet("{moniker}")]
        public async Task<ActionResult<CampModels>> Get(string moniker)
        {
            try
            {
                var result = await _campRepository.GetCampAsync(moniker);
                CampModels campModels = _mapper.Map<CampModels>(result);
                return campModels;
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Database Failure");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<CampModels[]>> SearchByDate(DateTime theDate)
        {
            try
            {
                var result = await _campRepository.GetAllCampsByEventDate(theDate);
                CampModels[] campModels = _mapper.Map<CampModels[]>(result);
                if (!campModels.Any()) {
                    return NotFound();
                }

                return campModels;
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Database Failure");
            }
        }
    }
}
