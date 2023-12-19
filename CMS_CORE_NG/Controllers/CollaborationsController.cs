using Microsoft.AspNetCore.Mvc;
using ModelService.windoModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using CMS_CORE_NG.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace CMS_CORE_NG.Controllers
{
    [Route("api/[controller]")]
    public class CollaborationsController : ControllerBase
    {
        private readonly CollaborationBl _bl;
        private readonly IMemoryCache _memoryCache;
        public CollaborationsController(CollaborationBl bl, IMemoryCache memoryCache)
        {
            _bl = bl;
            _memoryCache = memoryCache;
        }
        [HttpGet("getCaseStudyById")]
        public CaseStudyVM getCaseStudyById(int id)
        {
            try
            {
                CaseStudyVM CS = _bl.getCaseStudyById(id);
                return CS;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet("GetAllCaseStudy")]
        public List<CaseStudyForCardsVM> getAllCaseStudy()
        {
            try
            {
                if (!_memoryCache.TryGetValue(CasheKeyes.GetAllCaseStudyFromCashe, out List<CaseStudyForCardsVM> allCaseStudiesSFromCacheValue))
                {
                    allCaseStudiesSFromCacheValue = _bl.GetAllCS();
                    _memoryCache.Set(CasheKeyes.GetAllCaseStudyFromCashe, allCaseStudiesSFromCacheValue, new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromHours(3)));
                    if (allCaseStudiesSFromCacheValue.Count > 0)
                        return allCaseStudiesSFromCacheValue;
                    else
                        return null;
                }

                return allCaseStudiesSFromCacheValue;

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetLastCS")]
        public List<CaseStudyForCardsVM> GetLastCS()
        {
            try
            {
                if (!_memoryCache.TryGetValue(CasheKeyes.GetLastCSFromCashe, out List<CaseStudyForCardsVM> LastCaseStudiesSFromCacheValue))
                {
                    LastCaseStudiesSFromCacheValue = _bl.GetLastCS();
                    _memoryCache.Set(CasheKeyes.GetLastCSFromCashe, LastCaseStudiesSFromCacheValue, new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromHours(3)));
                    return LastCaseStudiesSFromCacheValue;
                }

                return LastCaseStudiesSFromCacheValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("getCaseStudyByCollaborationId")]
        public CaseStudyVM getCaseStudyByCollaborationId([FromBody] IdAndTableNameForCS idAndTableName)
        {
            try
            {
                CaseStudyVM CS = _bl.getCaseStudyByCollaborationId(idAndTableName);
                return CS;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("createCaseStudyWithPictures")]
        public async Task<int> createCaseStudyWithPictures()
        {
            try
            {
                var jsonModel = Request.Form.First(f => f.Key == "model").Value;
                CaseStudyVM model = JsonConvert.DeserializeObject<CaseStudyVM>(jsonModel);
                var files = Request.Form.Files;
                var mainPicture = files["mainPicture"];
                List<IFormFile> csFile = new List<IFormFile>();
                if (files.Length() > 0)
                {
                    for (int i = 0; i < files.Length(); i++)
                    {
                        if (files[i].Name == "files")
                            csFile.Add(files[i]);
                    }
                }

                int id = await _bl.createCaseStudyWithPictures(model, mainPicture, csFile);
                _memoryCache.Remove(CasheKeyes.GetAllCaseStudyFromCashe);
                _memoryCache.Remove(CasheKeyes.GetLastCSFromCashe);
                return id;
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpPost]
        [Route("updateCaseStudy")]
        public async Task< bool> updateCaseStudy()
        {
            try
            {
                var jsonModel = Request.Form.First(f => f.Key == "model").Value;
                CaseStudyVM model = JsonConvert.DeserializeObject<CaseStudyVM>(jsonModel);
                var files = Request.Form.Files;
                var mainPicture = files["mainPicture"];
                List<IFormFile> csFile = new List<IFormFile>();
                if (files.Length() > 0)
                {
                    for (int i = 0; i < files.Length(); i++)
                    {
                        if (files[i].Name == "files")
                            csFile.Add(files[i]);
                    }
                }

                if (model != null)
                {
                    bool ans = await _bl.updateCaseStudy(model,mainPicture,csFile);
                    _memoryCache.Remove(CasheKeyes.GetAllCaseStudyFromCashe);
                    _memoryCache.Remove(CasheKeyes.GetLastCSFromCashe);
                    return ans;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region
        [HttpGet("getAllPaidTransactions")]
        public List<PaidTransactionVM> getAllPaidTransactions()
        {
            try
            {
                return _bl.getAllPaidTransactions();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("getAllBarterDeals")]
        public List<BarterDealVM> getAllBarterDeals()
        {
            try
            {
                return _bl.getAllBarterDeals();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("getAllJointProjects")]
        public List<JointProjectVM> getAllJointProjects()
        {
            try
            {
                return _bl.getAllJointProjects();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        [HttpPost]
        [Route("GetCSByBuissinesID")]
        public List<CaseStudyForCardsVM> GetCSByBuissinesID(int BusinessID)
        {
            try
            {
                List<CaseStudyForCardsVM>CS = _bl.GetCSByBuissinesID(BusinessID);
                return CS;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("DeleteCollaborationByIDAndTable")]
        public bool DeleteCollaborationByIDAndTable(FromTable fromTable, int id)
        {
            return _bl.deleteCollaborationByIDAndTable(fromTable, id);
        }
    }


}
