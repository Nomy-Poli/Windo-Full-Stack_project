using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windo.Models;

using ModelService.windoModels;
using Microsoft.AspNetCore.Http;

namespace CMS_CORE_NG.BL
{
    public interface IAdvertismentBl
    {
        List<BannerVM> getBanners();
        List<ClientTypeVM> GetClientTypes();
        List<ClientVM> GetClients();

        List<CatalogServiceVM> GetCatalogServices();
        //List<WindoSiteServices> GetWindoSiteServices();
        List<ServiceTypeVM> GetWindoSiteServices();
        List<ServiceTypeVM> GetWindoSiteServicesById(int typeId);
        BannerVM GetBannerWithPic(int bannerId);
        OrderServiceWithAdDetailsVM getOrderServiceById(int orderId);
        List<OrderServiceVM> getOrders(int? status, int? clientId);
        public List<OrderStatusesVM> GetOrderStatuses();
        List<RequestOrderServiceVM> GetRequsetOrderService();
        Task <int>  CreateRequsetOrderService(RequestOrderServiceVM model);
        Task <bool> UpdateRequsetOrderServiceStatus(int requestId, int status);
        Task <int> CreateOrderAdvertisment(OrderServiceWithAdDetailsVM model);
        Task <bool> DeleteOrder(int orderId);
        Task<bool> DeleteBaner(int banerId);
        Task<bool> DeleteService(int serviceId);
        Task<bool> DeleteWindoSiteService(int siteServiceId);
        Task<bool> UpdateOrderAdvertisment(OrderServiceWithAdDetailsVM model, IFormFile picture);
        OrderServiceWithAdDetailsVM NewOrderServiceByRequest(int requestId);
        public ClientVM NewClientByRequest(int requestId);
        public ClientVM GetClient(int clientId);
        Task <int> CreateClient(ClientVM model);
        bool UpdateClient(ClientVM model);
        Task <bool> DeleteClient(int ClientId);

        public bool CheckIfAvalibleDate(IfAvalibleDate model);
        #region services and banner
        BannerVM GetBanner(int bannerId);
        Task<int> UpdateBanner(BannerVM model, IFormFile picture1, IFormFile picture2, IFormFile picture3);
        Task <int> CreateCatalogService(CatalogServiceVM model);
        Task <bool> UpdateCatalogService(CatalogServiceVM model);
        //Task<int> CreateWindoSiteServices(ServiceTypeVM model);
        bool UpdateWindoSiteServices(ServiceTypeVM model);
        Task<int> CreateWindoSiteServices(ServiceTypeVM model);

        #endregion

    }
}
