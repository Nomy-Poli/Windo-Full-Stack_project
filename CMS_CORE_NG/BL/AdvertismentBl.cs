using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ModelService.windoModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windo.Controllers;
using Windo.Models;
using Windo.Repository;
using AutoMapper;
using Windo.BL;
using CMS_CORE_NG.Repository;


namespace CMS_CORE_NG.BL
{
    public class AdvertismentBl : IAdvertismentBl
    {
        private readonly IAdvertismentRepo _repository;
        private readonly IConfiguration _configuration;
        public readonly IMapper _mapper;
        public readonly IBackGroungTask _bgTask;

        public AdvertismentBl(IMapper mapper, IAdvertismentRepo repository, IConfiguration configuration)
        {
            _mapper = mapper;
            _repository = repository;
            _configuration = configuration;
        }


        public BannerVM GetBannerWithPic(int bannerId)
        {
            return _mapper.Map<Banner,BannerVM>(_repository.GetBannerWithPic(bannerId));
        }

        public List<OrderServiceVM> getOrders(int? status, int? clientId)
        {
            return _repository.getOrders(status, clientId).Select(x => _mapper.Map<OrderServiceVM>(x)).ToList();
        }
        public List<OrderStatusesVM> GetOrderStatuses()
        {
            return _repository.GetOrderStatuses().Select(x => _mapper.Map<OrderStatusesVM>(x)).ToList();
        }
        public OrderServiceWithAdDetailsVM getOrderServiceById(int orderId)
        {
            return _mapper.Map<OrderServiceWithAdDetailsVM>(_repository.getOrderServiceById(orderId));
        }
        public List<RequestOrderServiceVM> GetRequsetOrderService()
        {
            var res= _repository.GetRequsetOrderService().Select(x => _mapper.Map<RequestOrderServiceVM>(x)).ToList();
            return res;
        }
        public async Task <int> CreateRequsetOrderService(RequestOrderServiceVM model)
        {
            try
            {
                model.CreationDate = DateTime.Now;
                model.RequestStatus = RequestStatus.waiting;
                int id = await _repository.CreateRequsetOrderService(_mapper.Map<RequestOrderServiceVM, RequestOrderService>(model));
                return id;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task <bool> UpdateRequsetOrderServiceStatus(int requestId, int status)
        {
            return await _repository.UpdateRequsetOrderServiceStatus(requestId, status);
        }
        public async Task <int> CreateOrderAdvertisment(OrderServiceWithAdDetailsVM model)
        {
            model.CreationDate = DateTime.Now;
            return await _repository.CreateOrderAdvertisment(_mapper.Map<OrderServiceWithAdDetailsVM, OrderService>(model));
        }

        public async Task<bool> UpdateOrderAdvertisment(OrderServiceWithAdDetailsVM model,IFormFile picture)
        {
            if(picture!= null)
            {
                string path = getPathAdvertisments();
                Guid? oldGuid = model.AdvertismentServiceOrder.PicGuid;
                model.AdvertismentServiceOrder.PicGuid = Guid.NewGuid();
                if(await SaveFileLocalDisc("",oldGuid,path+model.Makat,picture, (Guid)model.AdvertismentServiceOrder.PicGuid))
                {
                    Console.WriteLine("upload img = true");
                }
                else { 
                    Console.WriteLine("upload img = false");
                }
            }
            else if (model.AdvertismentServiceOrder.PicGuid == null)
            {
                model.AdvertismentServiceOrder.PicGuid = Guid.Empty;
            }
            if(model.Id>0)
                return await _repository.UpdateOrderAdvertisment(_mapper.Map<OrderServiceWithAdDetailsVM, OrderService>(model));
            else
            {
                _repository.CreateOrderAdvertisment(_mapper.Map<OrderServiceWithAdDetailsVM, OrderService>(model));
                return true;
            }
                
        }
        public async Task <bool> DeleteOrder(int orderId)
        {
            return await _repository.DeleteOrder(orderId);
        }
        public async Task<bool> DeleteBaner(int banerId)
        {
            return await _repository.DeleteBaner(banerId);
        }
        public async Task<bool> DeleteService(int serviceId)
        {
            return await _repository.DeleteService(serviceId);
        }
        public async Task<bool> DeleteWindoSiteService(int siteServiceId)
        {
            return await _repository.DeleteWindoSiteService(siteServiceId);
        }

        public List<BannerVM> getBanners()
        {
            return _repository.getBanners().Select(x => _mapper.Map<Banner, BannerVM>(x)).ToList();
        }

        public List<CatalogServiceVM> GetCatalogServices()
        {
            return _repository.GetCatalogServices().Select(x => _mapper.Map<CatalogService, CatalogServiceVM>(x)).ToList();
        }
        //public List<WindoSiteServices> GetWindoSiteServices()
        //{
        //    return _repository.GetWindoSiteServices();
        //}
        public List<ServiceTypeVM> GetWindoSiteServices()
        {
            return _repository.GetWindoSiteServices().Select(x => _mapper.Map<ServiceType, ServiceTypeVM>(x)).ToList();
        }
        public List<ServiceTypeVM> GetWindoSiteServicesById(int typeId)
        {
            return _repository.GetWindoSiteServicesById(typeId).Select(x => _mapper.Map<ServiceType, ServiceTypeVM>(x)).ToList();
        }
        public List<ClientTypeVM> GetClientTypes()
        {
            return _repository.GetClientTypes().Select(x => _mapper.Map<ClientType, ClientTypeVM>(x)).ToList();
        }

        public List<ClientVM> GetClients()
        {
            return _repository.GetClients().Select(x => _mapper.Map<Client, ClientVM>(x)).ToList();
        }


        public OrderServiceWithAdDetailsVM NewOrderServiceByRequest(int requestId)
        {
            return _mapper.Map<OrderServiceWithAdDetailsVM>(_repository.NewOrderServiceByRequest(requestId));
        }

        public bool CheckIfAvalibleDate(IfAvalibleDate model)
        {
            return _repository.CheckIfAvalibleDate(model);
        }

        #region Clients

        public ClientVM NewClientByRequest(int requestId)
        {
            return _mapper.Map<ClientVM>(_repository.NewClientByRequest(requestId));
        }
       
        public ClientVM GetClient(int clientId)
        {
            return _mapper.Map<ClientVM>(_repository.GetClient(clientId));
        }
        public async Task <int> CreateClient(ClientVM model)
        {
            model.CreationDate = DateTime.Now;
            return await _repository.CreateClient(_mapper.Map<Client>(model));
        }

        public bool UpdateClient(ClientVM model)
        {
            return _repository.UpdateClient(_mapper.Map<Client>(model));
        }

        public async Task <bool> DeleteClient(int ClientId)
        {
            return await _repository.DeleteClient(ClientId);
        }

#endregion
        #region files
        public async Task<bool> SaveFileLocalDisc(string Oldpath, Guid? oldGuide, string path, IFormFile file, Guid guidFile)
        {
            try
            {
                if (Directory.Exists(Oldpath))
                {
                    System.IO.DirectoryInfo di = new DirectoryInfo(Oldpath);
                    foreach (FileInfo Oldfile in di.GetFiles())
                    {
                        Oldfile.Delete();
                    }
                    //{
                    di.Delete(true);
                }
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (file.Length > 0)
                {
                    var FileName = guidFile.ToString() + ".jpg";
                    var filePath = Path.Combine(path, FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("error while saving file to disk : {0}", ex.Message);
                return false;
            }

        }
        private string getPathAdvertisments()
        {
            return _configuration["DiskPathAdvertisments"];
        }

        public async Task<int> UpdateBanner(BannerVM model, IFormFile defaultPicture, IFormFile examplePicture,IFormFile formatPicture)
        {
            if (defaultPicture != null)
            {
                string path = getPathAdvertisments() + model.Makat+ "\\default";
                Guid? oldGuid = model.DefaultPicGuid;
                model.DefaultPicGuid = Guid.NewGuid();
                if (await SaveFileLocalDisc("", oldGuid, path, defaultPicture, (Guid)model.DefaultPicGuid))
                {
                    Console.WriteLine("upload img = true");
                }
                else
                {
                    Console.WriteLine("upload img = false");
                }
            }
            else if (model.DefaultPicGuid == null)
            {
                model.DefaultPicGuid = Guid.Empty;
            }
            if (examplePicture != null)
            {
                string path = getPathAdvertisments() + model.Makat + "\\example";
                Guid? oldGuid = model.ExamplePicGuid;
                model.ExamplePicGuid = Guid.NewGuid();
                if (await SaveFileLocalDisc("", oldGuid, path, examplePicture, (Guid)model.ExamplePicGuid))
                {
                    Console.WriteLine("upload img = true");
                }
                else
                {
                    Console.WriteLine("upload img = false");
                }
            }
            else if (model.ExamplePicGuid == null)
            {
                model.ExamplePicGuid = Guid.Empty;
            }
            if (formatPicture != null)
            {
                string path = getPathAdvertisments() + model.Makat + "\\format";
                Guid? oldGuid = model.FormatPicGuid;
                model.FormatPicGuid = Guid.NewGuid();
                if (await SaveFileLocalDisc("", oldGuid, path, formatPicture, (Guid)model.FormatPicGuid))
                {
                    Console.WriteLine("upload img = true");
                }
                else
                {
                    Console.WriteLine("upload img = false");
                }
            }
            else if (model.FormatPicGuid == null)
            {
                model.FormatPicGuid = Guid.Empty;
            }
            if (model.Id > 0)
            {
                _repository.UpdateBanner(_mapper.Map<BannerVM, Banner>(model));
                return model.Id;
            }
            else
            {
                return _repository.CreateBanner(_mapper.Map<BannerVM, Banner>(model));
            }
        }

        public async Task <int> CreateCatalogService(CatalogServiceVM model)
        {
            return await _repository.CreateCatalogService(_mapper.Map<CatalogServiceVM, CatalogService>(model));
        }

        public async Task <bool> UpdateCatalogService(CatalogServiceVM model)
        {
            return await _repository.UpdateCatalogService(_mapper.Map<CatalogServiceVM, CatalogService>(model));

        }

        public async Task<int> CreateWindoSiteServices(ServiceTypeVM model)
        {
            return await _repository.CreateWindoSiteServices(_mapper.Map<ServiceTypeVM, ServiceType>(model));
        }
        public bool UpdateWindoSiteServices(ServiceTypeVM model)
        {
            return  _repository.UpdateWindoSiteServices(_mapper.Map<ServiceTypeVM, ServiceType>(model));
        }

        public BannerVM GetBanner(int bannerId)
        {
            return _mapper.Map<BannerVM>(_repository.GetBanner(bannerId));
        }
        #endregion

    }





}


