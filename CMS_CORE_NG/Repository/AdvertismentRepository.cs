using ModelService.windoModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService;
using ModelService;

namespace CMS_CORE_NG.Repository
{
    public class AdvertismentRepository : IAdvertismentRepo
    {
        private readonly ApplicationDbContext db;

        public AdvertismentRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task <int> CreateOrderAdvertisment(OrderService model)
        {
            try
            {
                model.CreationDate = DateTime.Now;
                db.OrderServices.AddAsync(model);
                db.SaveChanges();
                return model.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Banner> getBanners()
        {
            try
            {
                return db.Banners.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Banner GetBannerWithPic(int makat)
        {
            var banner = db.Banners.FirstOrDefault(b => b.Makat == makat);
            var order = db.AdvertismentServiceOrders
                .Include(x => x.OrderService)
                .Include(x => x.OrderService.Status)
                .FirstOrDefault(x => x.Makat == makat
                && x.adFromDate < DateTime.Now && x.adTillDate > DateTime.Now
                && x.OrderService.Status.Description == "אושר ושולם");
            if (order != null)
            {
                //option A 
                banner.AdvertismentServiceOrder = order;
                //option B
                //banner.AdvertismentPicGuid = order.PicGuid;
                //banner.AdvertismentLink = order.LinkToSite;
            }
            return banner;
        }

        

        public List<ClientType> GetClientTypes()
        {
            try
            {
                return db.ClientTypes.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public OrderService getOrderServiceById(int orderId)
        {
            try
            {
                return db.OrderServices
                    .Include(o => o.Client)
                    .Include(o => o.AdvertismentServiceOrder)
                    .Include(o => o.Status)
                    .FirstOrDefault(o => o.Id == orderId);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<OrderService> getOrders(int? status, int? clientId)
        {
            try
            {
                return db.OrderServices
                    .Include(o => o.AdvertismentServiceOrder)
                    .Include(o => o.Client)
                    .Include(o => o.Status)
                    .Filter(o => o.StatusOrderId > 0)
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<OrderStatuses> GetOrderStatuses()
        {
            return db.OrderStatuses.ToList();
        }
        public async Task <int> CreateRequsetOrderService(RequestOrderService model)
        {
            try
            {
               await db.RequsetsOrderService.AddAsync(model);
                db.SaveChanges();
                return model.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task <bool> UpdateRequsetOrderServiceStatus(int requestId, int status)
        {
            var request =await db.RequsetsOrderService.FindAsync(requestId);
            request.RequestStatus = status;
            db.RequsetsOrderService.Update(request);
            db.SaveChanges();
            return true;
        }

        public List<RequestOrderService> GetRequsetOrderService()
        {
            try
            {
                var res = db.RequsetsOrderService.ToList();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task <bool> UpdateOrderAdvertisment(OrderService model)
        {
            var order =await db.OrderServices.FindAsync(model.Id);
            order.StatusOrderId = model.StatusOrderId;
            order.Price = model.Price;
            order.Makat = model.Makat;
            order.AdvertismentServiceOrder = model.AdvertismentServiceOrder;
            db.OrderServices.Update(order);
            db.SaveChanges();
            return true;
        }

        public List<Client> GetClients()
        {
            try
            {
                return db.Clients.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public OrderService NewOrderServiceByRequest(int requestId)
        {
            var request = db.RequsetsOrderService.Find(requestId);
            request.RequestStatus = 2;
            //db.RequsetsOrderService.Update(request);
            //db.SaveChanges();
            var client = db.Clients.FirstOrDefault(c => c.Phone == request.Phone
            || c.Phone == request.Phone2
            || c.Email == request.Email);
            if (client == null)
            {
                client = new Client() { Id = 0, BusinessName = request.BusinessName, ContactName = request.ContactName, Phone = request.Phone, Email = request.Email, ClientTypeId = 1 };
                db.Clients.Add(client);
                db.SaveChanges();
            }
            var order = new OrderService()
            {
                Id = 0,
                ClientId = client.Id,
                Client = client,
                Makat = (int)request.Makat,
                StatusOrderId = 1,
                CreationDate = DateTime.Now,
                AdvertismentServiceOrder = new AdvertismentServiceOrder()
                {
                    Id = 0,
                    adFromDate = request.ServiceDate
                }
            };
            return order;
        }

        public bool CheckIfAvalibleDate(IfAvalibleDate model)
        {
            var order = db.OrderServices.Include(o => o.AdvertismentServiceOrder)
                .FirstOrDefault(o => o.Makat == model.Makat
                    && !(model.adTillDate < o.AdvertismentServiceOrder.adFromDate || model.adFromDate > o.AdvertismentServiceOrder.adTillDate)
                    && o.Id != model.OrderId);
            if (order != null)
            {
                return true;
            }
            else
                return false;
        }

        public async Task <bool> DeleteOrder(int orderId)
        {
            try
            {
                var order = await db.OrderServices.FindAsync(orderId);
                order.StatusOrderId = 0;
                db.OrderServices.Update(order);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<bool> DeleteBaner(int banerId)
        {
            try
            {
                var baner = await db.Banners.FindAsync(banerId);
                db.Banners.Remove(baner);
                 db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<bool> DeleteService(int serviceId)
        {
            try
            {
                var service = await db.CatalogServices.FindAsync(serviceId);
                //service.StatusOrderId = 0;
                db.CatalogServices.Remove(service);
               await db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<bool> DeleteWindoSiteService(int siteServiceId)
        {
            try
            {
                var siteServic = await db.ServiceTypes.FindAsync(siteServiceId);
                db.ServiceTypes.Remove(siteServic);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public Client GetClient(int clientId)
        {
            try
            {
                return db.Clients.Include(c => c.ClientType).FirstOrDefault(c => c.Id == clientId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Client NewClientByRequest(int requestId)
        {
            var request = db.RequsetsOrderService.Find(requestId);
            var client = db.Clients.FirstOrDefault(c => c.Phone == request.Phone
            || c.Phone == request.Phone2
            || c.Email == request.Email);
            if (client == null)
            {
                client = new Client() { Id = 0, BusinessName = request.BusinessName, ContactName = request.ContactName, Phone = request.Phone, Email = request.Email, ClientTypeId = 1 };
                db.Clients.Add(client);
                db.SaveChanges();
            }
            return client;
        }
        public async Task <int> CreateClient(Client model)
        {
            try
            {
                db.Clients.AddAsync(model);
                db.SaveChanges();
                return model.Id;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateClient(Client model)
        {
            try
            {
                var client = db.Clients.Find(model.Id);
                client.BusinessName = model.BusinessName;
                client.ContactName = model.ContactName;
                client.Phone = model.Phone;
                client.Email = model.Email;
                client.Description = model.Description;
                client.ClientTypeId = model.ClientTypeId;
                db.Clients.Update(client);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task <bool> DeleteClient(int ClientId)
        {
            try
            {
                var client = await db.Clients.FindAsync(ClientId);
                db.Clients.Remove(client);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region services and banner
        public Banner GetBanner(int bannerId)
        {
            var banner = db.Banners.Find(bannerId);
            return banner;
        }
        public int CreateBanner(Banner model)
        {
            try
            {
                db.Banners.Add(model);
                db.SaveChanges();
                return model.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool UpdateBanner(Banner model)
        {
            try
            {
                var banner = db.Banners.Find(model.Id);
                banner.Makat = model.Makat;
                banner.DefaultPicGuid = model.DefaultPicGuid;
                banner.ExamplePicGuid = model.ExamplePicGuid;
                banner.FormatPicGuid = model.FormatPicGuid;
                banner.PageID = model.PageID;
                banner.PageName = model.PageName;
                banner.Price = model.Price;
                banner.PriceInPoints = model.PriceInPoints;
                banner.Title = model.Title;
                banner.Width = model.Width;
                banner.Height = model.Height;
                banner.DefaultLink = model.DefaultLink;
                db.Banners.Update(banner);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public List<CatalogService> GetCatalogServices()
        {
            try
            {
                return db.CatalogServices.Include(c=>c.ServiceType).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ServiceType> GetWindoSiteServices()
        {
            try     
            {
                return db.ServiceTypes
                    .ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ServiceType> GetWindoSiteServicesById(int typeId)
        {
            try

            {
                return db.ServiceTypes
                    .Where(s => s.Id == typeId)
                    .ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task <int> CreateCatalogService(CatalogService model)
        {
            db.CatalogServices.AddAsync(model);
            db.SaveChanges();
            return model.Id;
        }

        public async Task <bool> UpdateCatalogService(CatalogService model)
        {
            try
            {
                var cat = await db.CatalogServices.FindAsync(model.Id);
                cat.Makat = model.Makat;
                cat.Description = model.Description;
                cat.ServiceTypeId = model.ServiceTypeId;
                db.CatalogServices.Update(cat);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateWindoSiteServices(ServiceType model)
        {
            try
            {
                var service = db.ServiceTypes.Find(model.Id);
                service.Id = model.Id;
                service.Name = model.Name;
                service.Description = model.Description;
                db.ServiceTypes.Update(service);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> CreateWindoSiteServices(ServiceType model)
        {

            db.ServiceTypes.AddAsync(model);
            db.SaveChanges();
            return model.Id;
        }
        #endregion
    }


}
