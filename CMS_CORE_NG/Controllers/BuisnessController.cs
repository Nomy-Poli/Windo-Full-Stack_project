using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelService.windoModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windo.BL;
using Windo.Models;

using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Microsoft.Extensions.Caching.Memory;
using EmailService;
using Serilog;
using Microsoft.Extensions.Configuration;


namespace Windo.Controllers
{
    [Route("api/[controller]")]
    public class BuisnessController : ControllerBase
    {
        public readonly BuisnessBl _bl;
        private readonly IMemoryCache _memoryCache;
        private readonly IEmailSvc _emailSvc;
        private readonly IConfiguration _configuration;

        public BuisnessController(BuisnessBl bl, IMemoryCache memoryCache, 
            IEmailSvc emailSvc, IConfiguration configuration)
        {
            _bl = bl;
            _memoryCache = memoryCache;
            _emailSvc = emailSvc;
            _configuration = configuration;
        }
        [HttpGet("GetBuisnessByEmailId")]
        public BuisnessVm GetBuisnessByEmailId(string email,int? currentBusinessId)
        {
            try
            {
                if (email != null)
                {
                    BuisnessVm buisness = _bl.GetBuisnessByEmailId(email, currentBusinessId);

                    if (buisness != null)
                    {
                        return buisness;
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("GetCurrentBuisnessByEmail")]
        public BusinessNamesPicUserIdVM GetCurrentBuisnessByEmail(string email)
        {
            try
            {
                if (email != null)
                {
                    BusinessNamesPicUserIdVM buisness = _bl.GetCurrentBuisnessByEmail(email);

                    if (buisness != null)
                    {
                        return buisness;
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetListOfBuisnesses")]
        public List<BusinessForCardVM> GetListOfBuisnesses()
        {
            try
            {
                if (!_memoryCache.TryGetValue(CasheKeyes.GetListOfBuisnessesFromCashe, out List<BusinessForCardVM> BFCacheValue))
                {
                    BFCacheValue = _bl.GetListOfBuisnesses();
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

        [HttpGet("GetListOfLatestUpdatedBuisnesses")]
        public List<BusinessForCardVM> GetListOfLatestUpdatedBuisnesses()
        {
            try
            {
                if (!_memoryCache.TryGetValue(CasheKeyes.LatestBuisnessesFromCashe, out List<BusinessForCardVM> LBFCacheValue))
                {
                    LBFCacheValue = _bl.GetListOfLatestUpdatedBuisnesses();
                    _memoryCache.Set(CasheKeyes.LatestBuisnessesFromCashe, LBFCacheValue, new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromHours(3)));
                    return LBFCacheValue;
                }

                //return  GetListOfBuisnesses(true);
                return LBFCacheValue;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        [HttpGet("GetBusinessNamesPictures")]
        public List<BusinessNamesPicturesVM> GetBusinessNamesPictures()
        {
            try
            {
                return _bl.GetBusinessNamesPictures();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        [HttpGet("GetBusinessNamesPictUser")]
        public List<BusinessNamesPicUserIdVM> GetBusinessNamesPictUser()
        {
            try
            {
                return _bl.GetBusinessNamesPictUser();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        [HttpGet("deleteBuisness")]
        public int deleteBuisness(int id)
        {
            try
            {
                int success = -1;
                success = _bl.deleteBuisness(id);
                if (id >= 0)
                    return success;
                else
                    return -1;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpGet("GetAreasList")]
        public List<AreaVm> GetAreasList()
        {
            try
            {
                List<AreaVm> areaList = new List<AreaVm>();
                areaList = _bl.GetAreasList();
                if (areaList != null)
                    return areaList;
                else
                    return null;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        //the true func
        [HttpPost]
        [Route("createBuisnessWithFiles")]
        public async Task<int> createBuisnessWithFiles()
        {
            try
            {
              
                var jsonModel = Request.Form.First(f => f.Key == "model").Value;
                BuisnessVm model = JsonConvert.DeserializeObject<BuisnessVm>(jsonModel);
                var files = Request.Form.Files;
                model.coverPicture = files["coverPicture"];
                model.logoPicture = files["logoPicture"];
                List<IFormFile> workFile = new List<IFormFile>();
                if (files.Length() > 0)
                {
                    for (int i = 0; i < files.Length(); i++)
                    {
                        if (files[i].Name == "files")
                            workFile.Add(files[i]);
                    }
                }
                int id = -1;
                if (model != null)
                {
                    id = await _bl.createBuisnessWithFiles(model, workFile);

                    _memoryCache.Remove(CasheKeyes.GetListOfBuisnessesFromCashe);
                    _memoryCache.Remove(CasheKeyes.LatestBuisnessesFromCashe);
                }
                if (id >= 0)
                    return id;
                else
                    return -1;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #region collaborations

        //paid transaction

        [HttpPost]
        [Route("createPaidTransactionWithPicture")]
        public async Task<int> createPaidTransactionWithPicture()
        {
            try
            {
                var jsonModel = Request.Form.First(f => f.Key == "model").Value;
                PaidTransactionVM model = JsonConvert.DeserializeObject<PaidTransactionVM>(jsonModel);
                var files = Request.Form.Files;
                model.PaidTransactionPicture = files["PaidTransactionPicture"];
                int id = -1;
                if (model != null)
                {
                    id = await _bl.createPaidTransactionWithPicture(model);
                    model.Id = id;
                    await SendEmailToManager(model);
                }
                if (id >= 0)
                    return id;
                else
                    return -1;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpPost]
        [Route("CreateBarterDealWithPictures")]
        public async Task<int> CreateBarterDealWithPictures()
        {
            try
            {
                var jsonModel = Request.Form.First(f => f.Key == "model").Value;
                BarterDealVM model = JsonConvert.DeserializeObject<BarterDealVM>(jsonModel);
                var files = Request.Form.Files;
                model.Business1Picture = files["reportPicture"];
                model.Business2Picture = files["partnerPicture"];
                int id = -1;
                if (model != null)
                {
                    id = await _bl.CreateBarterDealWithPictures(model);
                    model.Id = id;
                    await SendEmailToManager(model);
                }
                if (id >= 0)
                    return id;
                else
                    return -1;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpPost]
        [Route("createBarterDeal")]
        public async Task<int> createBarterDeal([FromBody] BarterDealVM model)
        {
            try
            {
                if (model != null)
                {
                    return _bl.createBarterDeal(model);
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Route("createPaidTransaction")]
        public async Task<int> createPaidTransaction([FromBody]PaidTransactionVM model)
        {
            try
            {
                if (model != null)
                {
                    return _bl.createPaidTransaction(model);
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Route("createJoinProjectWithPictures")]
        public async Task<int> createJoinProjectWithPictures()
        {
            try
            {
                var jsonModel = Request.Form.First(f => f.Key == "model").Value;
                JointProjectVM model = JsonConvert.DeserializeObject<JointProjectVM>(jsonModel);
                var files = Request.Form.Files;
                model.Picture = files["Picture"];
                int id = -1;
                if (model != null)
                {
                    id = await _bl.createJointProjectWithPictures(model);
                    model.Id = id;
                    await SendEmailToManager(model);
                }
                if (id >= 0)
                    return id;
                else
                    return -1;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Route("CreateBusinessInCollaboration")]
        public int BusinessInCollaboration([FromBody] BusinessInCollaborationVM model)
        {
            return 1;
        }
        [HttpPost]
        [Route("CreateJointProject")]
        public int CreateJointProject([FromBody] JointProjectVM model)
        {
            return 1;
        }
        [HttpPost]
        [Route("createCollaborationType")]
        public int createCollaborationType([FromBody]CollaborationTypeVM collaborationType)
        {
            try
            {
                if (collaborationType != null)
                {
                    return _bl.createCollaborationType(collaborationType);
                }
                else
                    return -1;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        [Route("GetCollaborationTypes")]
        public List<CollaborationTypeVM> GetCollaborationTypes()
        {
            return _bl.GetCollaborationTypes();
        }
        
        public async Task<bool> SendEmailToManager(object model)
        {
            var managerEmail = "office@windo.org.il";
            var subject = "נוצר שיתוף פעולה חדש באתר ווינדו";
            var callBackURL = Url.ActionLink("", "collaboration-list", protocol: HttpContext.Request.Scheme);
            await _emailSvc.SendEmailAsync(managerEmail, subject, callBackURL, "NewCollaborationCreated.html",null);
            Log.Information($"Email About New Collaboration Was Sent => { model }");
            return true;
        }
        public string GetManageEmail()
        {
            return _configuration["ManagerEmail"];
        }
        #endregion
    }
    //public class BuisnessWithFilesVm
    //{
    //    public int id { get; set; }
    //    public string userId { get; set; }
    //    public string buisnessName { get; set; }
    //    public string phoneNumber1 { get; set; }
    //    public string phoneNumber2 { get; set; }
    //    public string address { get; set; }
    //    public string actionDiscription { get; set; }//שורת פעולה
    //    public string discription { get; set; }//תיאור מפורט
    //    public string buisnessWebSiteLink { get; set; }//לינק לאתר
    //    public bool? isdisplayBusinessOwnerName { get; set; }//האם להציג את שם בעלת העסק
    //    public bool? ispayingBuisness { get; set; }//שיטת עסקים ע"י תשלום
    //    public bool? isburterBuisness { get; set; }//שיטת עסקים ע"י בארטר
    //    public bool? iscollaborationBuisness { get; set; }//שיטת עסקים ע"י שת"פ
    //    public IFormFile coverPicture { get; set; } //string that will become a guid
    //    public IFormFile logoPicture { get; set; }//string that will become a guid
    //    //public virtual List<BuisnessPictureVm> BuisnessPictureVm { get; set; }
    //}
}