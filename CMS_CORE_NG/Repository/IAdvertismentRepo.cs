using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windo.Models;
using ModelService.windoModels;

namespace CMS_CORE_NG.Repository
{
    public interface IAdvertismentRepo
    {
        Banner GetBannerWithPic(int bannerId);
        List<Banner> getBanners();
        List<ClientType> GetClientTypes();
        List<Client> GetClients();
        List<CatalogService> GetCatalogServices();
        //List<WindoSiteServices> GetWindoSiteServices();
        List<ServiceType> GetWindoSiteServicesById(int typeId);
        List<ServiceType> GetWindoSiteServices();
        OrderService getOrderServiceById(int orderId);
        List<OrderService> getOrders(int? status, int? clientId);
        public List<OrderStatuses> GetOrderStatuses();
        List<RequestOrderService> GetRequsetOrderService();
        Task <int> CreateRequsetOrderService(RequestOrderService model);
        Task <bool> UpdateRequsetOrderServiceStatus(int requestId, int status);
         Task <int> CreateOrderAdvertisment(OrderService model);
        Task<bool> UpdateOrderAdvertisment(OrderService model);
        Task <bool> DeleteOrder(int orderId);
        Task<bool> DeleteBaner(int banerId);
        Task<bool> DeleteService(int serviceId);
        Task<bool> DeleteWindoSiteService(int siteServiceId);
        OrderService NewOrderServiceByRequest(int requestId);

        public Client NewClientByRequest(int requestId);
        public Client GetClient(int clientId);
        Task <int> CreateClient(Client model);
        bool UpdateClient(Client model);
        Task <bool> DeleteClient(int ClientId);

        public bool CheckIfAvalibleDate(IfAvalibleDate model);

        #region services and banner
        Banner GetBanner(int bannerId);
        int CreateBanner(Banner model);
        bool UpdateBanner(Banner model);

        Task <int> CreateCatalogService(CatalogService model);
        Task <bool> UpdateCatalogService(CatalogService model);
        Task<int> CreateWindoSiteServices(ServiceType model);
        bool UpdateWindoSiteServices(ServiceType model);
        #endregion
    }
}
