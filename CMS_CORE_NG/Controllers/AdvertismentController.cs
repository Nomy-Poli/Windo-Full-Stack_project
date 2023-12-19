using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelService.windoModels;
using Newtonsoft.Json;
using System;
using EmailService;
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
using CMS_CORE_NG.BL;
using ModelService.windoModels.templates;

namespace CMS_CORE_NG.Controllers
{
    [Route("api/[controller]")]
    public class AdvertismentController : ControllerBase
    {
        private readonly IAdvertismentBl _bl;
        private readonly IMemoryCache _memoryCache;
        private readonly IEmailSvc _emailSvc;
        private readonly IConfiguration _configuration;

        public AdvertismentController(IAdvertismentBl bl, IMemoryCache memoryCache,
            IEmailSvc emailSvc, IConfiguration configuration)
        {
            _bl = bl;
            _memoryCache = memoryCache;
            _emailSvc = emailSvc;
            _configuration = configuration;
        }

        [HttpGet("GetBannerWithPic")]
        public BannerVM GetBannerWithPic(int makat)
        {
            if (!_memoryCache.TryGetValue(CasheKeyes.BannerWithPic+makat, out BannerVM bannerCacheValue))
            {
                bannerCacheValue = _bl.GetBannerWithPic(makat);
                if (bannerCacheValue != null && bannerCacheValue.AdvertismentServiceOrder != null)
                {
                    _memoryCache.Set(CasheKeyes.BannerWithPic+makat, bannerCacheValue, new MemoryCacheEntryOptions()
                            .SetAbsoluteExpiration(DateTimeOffset.Parse(bannerCacheValue.AdvertismentServiceOrder.adTillDate.ToString())));
                }
            }
            return bannerCacheValue;
        }
        
        
        #region orders
        [HttpGet("getOrders")]
        public List<OrderServiceVM> getOrders(int? status, int? clientId)
        {
            return _bl.getOrders(status, clientId);
        }
        [HttpGet("GetOrderStatuses")]
        public List<OrderStatusesVM> GetOrderStatuses()
        {
            return _bl.GetOrderStatuses();
        }

        [HttpGet("getOrderServiceById")]
        public OrderServiceWithAdDetailsVM getOrderServiceById(int orderId)
        {
            return _bl.getOrderServiceById(orderId);
        }

        [HttpPost]
        [Route("CreateOrderAdvertisment")]
        public async Task <int> CreateOrderAdvertisment([FromBody] OrderServiceWithAdDetailsVM model)
        {
            try
            {
                return await  _bl.CreateOrderAdvertisment(model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("UpdateOrderAdvertisment")]

        public async Task<bool> UpdateOrderAdvertisment()
        {
            var jsonModel = Request.Form.First(f => f.Key == "model").Value;
            OrderServiceWithAdDetailsVM model = JsonConvert.DeserializeObject<OrderServiceWithAdDetailsVM>(jsonModel);
            var files = Request.Form.Files;
            var picture = files["picture"];
            var id = await _bl.UpdateOrderAdvertisment(model, picture);
            _memoryCache.Remove(CasheKeyes.BannerWithPic + model.Makat);
            return id;
        }

        [HttpGet("DeleteOrder")]
        public async Task <bool> DeleteOrder(int orderId)
        {
            return await _bl.DeleteOrder(orderId);
        }
        [HttpGet("DeleteBaner")]
        public async Task<bool> DeleteBaner(int banerId)
        {
            return await _bl.DeleteBaner(banerId);
        }
        [HttpGet("DeleteService")]
        public async Task<bool> DeleteService(int serviceId)
        {
            return await _bl.DeleteService(serviceId);
        }
        [HttpGet("DeleteWindoSiteService")]
        public async Task<bool> DeleteWindoSiteService(int siteServiceId)
        {
            return await _bl.DeleteService(siteServiceId);
        }

        [HttpGet("NewOrderServiceByRequest")]
        public OrderServiceWithAdDetailsVM NewOrderServiceByRequest(int requestId)
        {
            return _bl.NewOrderServiceByRequest(requestId);
        }
        [HttpPost]
        [Route("CheckIfAvalibleDate")]
        public bool CheckIfAvalibleDate([FromBody] IfAvalibleDate model)
        {
            return _bl.CheckIfAvalibleDate(model);
        }

        #endregion


        #region requests
        [HttpGet("GetRequsetOrderService")]
        public List<RequestOrderServiceVM> GetRequsetOrderService()
        {
            var res= _bl.GetRequsetOrderService();
            return res;
        }

        [HttpPost]
        [Route("CreateRequsetOrderService")]
        public async Task <int> CreateRequsetOrderService([FromBody] RequestOrderServiceVM model)
        {
            try
            {
               var result= await _bl.CreateRequsetOrderService(model);
                var subject = $"התקבלה בקשה להזמנת פירסום מ {model.BusinessName}";
                //link to the banner
                var callBackURL = Url.ActionLink("", "services-manager-area", null, protocol: HttpContext.Request.Scheme);
                var moreText = model.Text +","+ " הפרטים שלי ליצירת קשר : " +" "+ model.Email+" , " +model.Phone;
               await _emailSvc.SendEmailAsync("office@windo.org.il", subject, callBackURL, "NewOrderBanner.html", moreText);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("UpdateRequsetOrderServiceStatus")]
        public async Task <bool> UpdateRequsetOrderServiceStatus(int requestId, int status)
        {
            return await  _bl.UpdateRequsetOrderServiceStatus(requestId, status);
        }

        [HttpPost]
        [Route("OpenOrderServiceFromRequest")]
        public int OpenOrderServiceFromRequest([FromBody] RequestOrderServiceVM model)
        {
            try
            {
                return 1;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        
        [HttpGet("GetCatalogServices")]
        public List<CatalogServiceVM> GetCatalogServices()
        {
            return _bl.GetCatalogServices();
        }
      
        //public List<WindoSiteServices> GetWindoSiteServices()
        //{
        //    return _bl.GetWindoSiteServices();
        //}
        [HttpGet("GetWindoSiteServices")]
        public List<ServiceTypeVM> GetWindoSiteServices()
        {
            return _bl.GetWindoSiteServices();
        }
        [HttpGet("GetWindoSiteServicesById")]
        public List<ServiceTypeVM> GetWindoSiteServicesById(int typeId)
        {
            return _bl.GetWindoSiteServicesById(typeId);
        }

        #region clients

        [HttpGet("GetClientTypes")]
        public List<ClientTypeVM> GetClientTypes()
        {
            return _bl.GetClientTypes();
        }

        [HttpGet("GetClients")]
        public List<ClientVM> GetClients()
        {
            return _bl.GetClients();
        }

        [HttpGet("NewClientByRequest")]
        public ClientVM NewClientByRequest(int requestId)
        {
            return _bl.NewClientByRequest(requestId);
        }
        [HttpGet("GetClient")]
        public ClientVM GetClient(int clientId)
        {
            return _bl.GetClient(clientId);
        }
        [HttpPost]
        [Route("CreateClient")]
        public async Task <int> CreateClient([FromBody]ClientVM model)
        {
            return await _bl.CreateClient(model);
        }
        [HttpPost]
        [Route("UpdateClient")]
        public bool UpdateClient([FromBody] ClientVM model)
        {
            return _bl.UpdateClient(model);
        }
        [HttpGet("DeleteClient")]
        public async Task <bool> DeleteClient(int clientId)
        {
            return await _bl.DeleteClient(clientId);
        }

        #endregion


        #region services
        [HttpGet("getBanners")]
        public List<BannerVM> getBanners()
        {
            return _bl.getBanners();
        }
        [HttpGet("GetBanner")]
        public BannerVM GetBanner(int bannerId)
        {
            return _bl.GetBanner(bannerId);
        }
        [HttpPost]
        [Route("UpdateBanner")]

        public async Task<int> UpdateBanner()
        {
            var jsonModel = Request.Form.First(f => f.Key == "model").Value;
            BannerVM model = JsonConvert.DeserializeObject<BannerVM>(jsonModel);
            var files = Request.Form.Files;
            var defaultPicture = files["defaultPicture"];
            var examplePicture = files["examplePicture"];
            var formatPicture = files["formatPicture"];
            var id = await _bl.UpdateBanner(model, defaultPicture, examplePicture, formatPicture);
            _memoryCache.Remove(CasheKeyes.BannerWithPic + model.Makat);
            return id;
        }
        [HttpPost]
        [Route("CreateCatalogService")]
        public async Task <int> CreateCatalogService([FromBody]CatalogServiceVM model)
        {
            return await  _bl.CreateCatalogService(model);
        }
        [HttpPost]
        [Route("UpdateCatalogService")]
        public async Task <bool> UpdateCatalogService([FromBody] CatalogServiceVM model)
        {
            return await _bl.UpdateCatalogService(model);

        }

        [HttpPost]
        [Route("CreateWindoSiteServices")]
        public async Task<int> CreateWindoSiteServices([FromBody] ServiceTypeVM model)
        {
            return await _bl.CreateWindoSiteServices(model);
        }

        [HttpPost]
        [Route("UpdateWindoSiteServices")]
        public bool UpdateWindoSiteServices([FromBody] ServiceTypeVM model)
        {
            return _bl.UpdateWindoSiteServices(model);
        }

        #endregion
    }
}
