

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS_CORE_NG.BL;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Microsoft.Extensions.Caching.Memory;
using EmailService;
using AutoMapper.Configuration;
using ModelService.windoModels;
using static CMS_CORE_NG.Scoring;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;

namespace CMS_CORE_NG.Controllers
{
    [Route("api/[controller]")]
    public class ScoringController : ControllerBase
    {
        private readonly IScoringBl _bl;
        private readonly IMemoryCache _memoryCache;
        private readonly IEmailSvc _emailSvc;
        //private readonly IConfiguration _configuration;

        public ScoringController(IScoringBl bl, IMemoryCache memoryCache,
            IEmailSvc emailSvc)//, IConfiguration configuration
        {
            _bl = bl;
            _memoryCache = memoryCache;
            _emailSvc = emailSvc;
            //_configuration = configuration;
        }
        //[HttpPut]
        //[Route("ResetCount")]
       [HttpGet("ResetCount")]
        public async Task<bool> ResetCount(int buisnessId)
        {
            _memoryCache.Remove(CasheKeyes.GetListOfBuisnessesFromCashe);
            return await _bl.ResetCount(buisnessId);
        }


        [HttpGet("getScroingOperation")]
        public List<ScroingOperationVM> getScroingOperation(int typeId)
        {

            var so = _bl.getScroingOperation(typeId);
            return so;
        }
        [HttpGet("DeleteScroingOperation")]
        public async Task<bool> DeleteScroingOperation(int operitionId)
        {
            return await _bl.DeleteScroingOperation(operitionId);
        }

        [HttpPost]
        [Route("CreateScroingOperation")]

        public async Task<int> CreateScroingOperation([FromBody] ScroingOperationVM model)
        {
            return await _bl.CreateScroingOperation(model);
        }
        [HttpPost]
        [Route("UpdateScroingOperation")]
        public async Task<bool> UpdateScroingOperation([FromBody] ScroingOperationVM model)
        {
            return await _bl.UpdateScroingOperation(model);
        }

        [HttpGet("GetListOfBuisnessesWithScoring")]
        public List<BusinessForScoringVM> GetListOfBuisnessesWithScoring()
        {
            try
            {
                if (!_memoryCache.TryGetValue(CasheKeyes.GetListOfBuisnessesFromCashe, out List<BusinessForScoringVM> BFCacheValue))
                {
                    BFCacheValue = _bl.GetListOfBuisnessesWithScoring();
                    _memoryCache.Set(CasheKeyes.GetListOfBuisnessesFromCashe, BFCacheValue, new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromHours(3)));
                    return BFCacheValue;
                }

                return BFCacheValue;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [HttpGet("GetScoringActions")]
        public List<ScoringAction> GetScoringActions()
        {
            try
            {

                var sa = _bl.GetScoringActions();
                return sa;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpGet("GetBusinessScoringsDetailById")]
        public List<BusinessScoringsDetailVM> GetBusinessScoringsDetailById(int buisnessId)
        {
            try
            {

                var bs = _bl.GetBusinessScoringsDetailById(buisnessId);
                return bs;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [HttpGet("GetScoreToBusiness")]
        public bool GetScoreToBusiness(int scoringActionId, int buisnessId)
        {
            return _bl.GetScoreToBusiness(scoringActionId, buisnessId);

        }
        [HttpPost]
        [Route("AddMultipleActions")]
        public bool AddMultipleActions([FromBody] MultipleActions model)
        {
            return _bl.AddMultipleActions(model);
        }
    }
}

